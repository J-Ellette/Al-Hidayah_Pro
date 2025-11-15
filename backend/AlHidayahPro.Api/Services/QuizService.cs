using System.Text.Json;
using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for quiz functionality
/// </summary>
public class QuizService : IQuizService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILearningService _learningService;
    private readonly ILogger<QuizService> _logger;

    public QuizService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILearningService learningService,
        ILogger<QuizService> logger)
    {
        _dbFactory = dbFactory;
        _learningService = learningService;
        _logger = logger;
    }

    public async Task<IEnumerable<Quiz>> GetQuizzesAsync(string? category = null, string? difficulty = null)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        IQueryable<Quiz> query = context.Quizzes
            .Where(q => q.IsActive);

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(q => q.Category == category);
        }

        if (!string.IsNullOrEmpty(difficulty))
        {
            query = query.Where(q => q.DifficultyLevel == difficulty);
        }

        return await query.Include(q => q.Questions).ToListAsync();
    }

    public async Task<Quiz?> GetQuizAsync(int quizId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.Id == quizId);
    }

    public async Task<QuizAttempt> StartQuizAsync(int userId, int quizId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var quiz = await GetQuizAsync(quizId);
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found", nameof(quizId));
        }

        var attempt = new QuizAttempt
        {
            UserId = userId,
            QuizId = quizId,
            StartTime = DateTime.UtcNow,
            TotalQuestions = quiz.Questions.Count
        };

        context.QuizAttempts.Add(attempt);
        await context.SaveChangesAsync();

        return attempt;
    }

    public async Task<QuizAttempt> SubmitQuizAsync(int attemptId, Dictionary<int, string> answers)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var attempt = await context.QuizAttempts
            .Include(a => a.Quiz)
                .ThenInclude(q => q!.Questions)
            .FirstOrDefaultAsync(a => a.Id == attemptId);

        if (attempt == null)
        {
            throw new ArgumentException("Quiz attempt not found", nameof(attemptId));
        }

        if (attempt.EndTime.HasValue)
        {
            throw new InvalidOperationException("Quiz already submitted");
        }

        attempt.EndTime = DateTime.UtcNow;
        attempt.TimeTakenSeconds = (int)(attempt.EndTime.Value - attempt.StartTime).TotalSeconds;
        attempt.Answers = JsonSerializer.Serialize(answers);

        // Grade the quiz
        int correctCount = 0;
        if (attempt.Quiz?.Questions != null)
        {
            foreach (var question in attempt.Quiz.Questions)
            {
                if (answers.TryGetValue(question.Id, out var userAnswer))
                {
                    if (string.Equals(userAnswer?.Trim(), question.CorrectAnswer?.Trim(), 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                    }
                }
            }
        }

        attempt.CorrectAnswers = correctCount;
        attempt.Score = attempt.TotalQuestions > 0 
            ? (decimal)correctCount * 100 / attempt.TotalQuestions 
            : 0;
        attempt.Passed = attempt.Score >= (attempt.Quiz?.PassingScore ?? 70);

        await context.SaveChangesAsync();

        // Update user progress
        await _learningService.IncrementProgressMetricAsync(attempt.UserId, "quizzes_completed");
        
        // Update average quiz score
        var userProgress = await _learningService.GetUserProgressAsync(attempt.UserId);
        if (userProgress != null)
        {
            var allAttempts = await context.QuizAttempts
                .Where(a => a.UserId == attempt.UserId && a.EndTime.HasValue)
                .ToListAsync();
                
            userProgress.AverageQuizScore = allAttempts.Count > 0 
                ? allAttempts.Average(a => a.Score) 
                : 0;
                
            await _learningService.UpdateUserProgressAsync(userProgress);
        }

        _logger.LogInformation("Quiz {QuizId} submitted by user {UserId} with score {Score}%", 
            attempt.QuizId, attempt.UserId, attempt.Score);

        return attempt;
    }

    public async Task<IEnumerable<QuizAttempt>> GetUserQuizHistoryAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.QuizAttempts
            .Where(a => a.UserId == userId)
            .Include(a => a.Quiz)
            .OrderByDescending(a => a.StartTime)
            .ToListAsync();
    }
}
