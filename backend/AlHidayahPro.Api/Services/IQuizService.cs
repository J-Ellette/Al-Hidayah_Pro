using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for quiz functionality
/// </summary>
public interface IQuizService
{
    Task<IEnumerable<Quiz>> GetQuizzesAsync(string? category = null, string? difficulty = null);
    Task<Quiz?> GetQuizAsync(int quizId);
    Task<QuizAttempt> StartQuizAsync(int userId, int quizId);
    Task<QuizAttempt> SubmitQuizAsync(int attemptId, Dictionary<int, string> answers);
    Task<IEnumerable<QuizAttempt>> GetUserQuizHistoryAsync(int userId);
}
