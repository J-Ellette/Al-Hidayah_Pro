using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for learning features
/// </summary>
public class LearningService : ILearningService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly IAiService _aiService;
    private readonly ILogger<LearningService> _logger;

    public LearningService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        IAiService aiService,
        ILogger<LearningService> logger)
    {
        _dbFactory = dbFactory;
        _aiService = aiService;
        _logger = logger;
    }

    public async Task<IEnumerable<LearningPath>> GetUserLearningPathsAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.LearningPaths
            .Where(lp => lp.UserId == userId)
            .Include(lp => lp.Milestones)
            .OrderByDescending(lp => lp.IsActive)
            .ThenByDescending(lp => lp.StartDate)
            .ToListAsync();
    }

    public async Task<LearningPath?> GetLearningPathAsync(int pathId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.LearningPaths
            .Include(lp => lp.Milestones)
            .FirstOrDefaultAsync(lp => lp.Id == pathId);
    }

    public async Task<LearningPath> CreateLearningPathAsync(LearningPath path)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        context.LearningPaths.Add(path);
        await context.SaveChangesAsync();
        return path;
    }

    public async Task<LearningPath> UpdateLearningPathAsync(LearningPath path)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        context.LearningPaths.Update(path);
        await context.SaveChangesAsync();
        return path;
    }

    public async Task<bool> DeleteLearningPathAsync(int pathId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var path = await context.LearningPaths.FindAsync(pathId);
        if (path == null) return false;
        
        context.LearningPaths.Remove(path);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<UserProgress?> GetUserProgressAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        var progress = await context.UserProgresses
            .FirstOrDefaultAsync(up => up.UserId == userId);
            
        if (progress == null)
        {
            // Create default progress for new user
            progress = new UserProgress
            {
                UserId = userId,
                Level = 1,
                LastActivityDate = DateTime.UtcNow
            };
            context.UserProgresses.Add(progress);
            await context.SaveChangesAsync();
        }
        
        return progress;
    }

    public async Task UpdateUserProgressAsync(UserProgress progress)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        progress.LastActivityDate = DateTime.UtcNow;
        context.UserProgresses.Update(progress);
        await context.SaveChangesAsync();
    }

    public async Task IncrementProgressMetricAsync(int userId, string metricType, int amount = 1)
    {
        var progress = await GetUserProgressAsync(userId);
        if (progress == null) return;

        switch (metricType.ToLowerInvariant())
        {
            case "verses_read":
                progress.VersesRead += amount;
                break;
            case "verses_memorized":
                progress.VersesMemorized += amount;
                break;
            case "hadiths_studied":
                progress.HadithsStudied += amount;
                break;
            case "lessons_completed":
                progress.LessonsCompleted += amount;
                break;
            case "quizzes_completed":
                progress.QuizzesCompleted += amount;
                break;
            case "study_minutes":
                progress.TotalStudyMinutes += amount;
                break;
        }

        await UpdateUserProgressAsync(progress);
    }

    public async Task<IEnumerable<Achievement>> GetAllAchievementsAsync()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.Achievements
            .Where(a => !a.IsHidden)
            .OrderBy(a => a.Category)
            .ThenBy(a => a.Points)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserAchievement>> GetUserAchievementsAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.UserAchievements
            .Where(ua => ua.UserId == userId)
            .Include(ua => ua.Achievement)
            .OrderByDescending(ua => ua.EarnedDate)
            .ToListAsync();
    }

    public async Task<UserAchievement?> AwardAchievementAsync(int userId, string achievementId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == achievementId);
            
        if (achievement == null) return null;

        // Check if already earned
        var existing = await context.UserAchievements
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AchievementId == achievement.Id);
            
        if (existing != null) return existing;

        var userAchievement = new UserAchievement
        {
            UserId = userId,
            AchievementId = achievement.Id,
            EarnedDate = DateTime.UtcNow
        };

        context.UserAchievements.Add(userAchievement);
        
        // Award points to user progress
        var progress = await GetUserProgressAsync(userId);
        if (progress != null)
        {
            progress.TotalPoints += achievement.Points;
            progress.ExperiencePoints += achievement.Points;
            await UpdateUserProgressAsync(progress);
        }

        await context.SaveChangesAsync();
        return userAchievement;
    }

    public async Task<IEnumerable<LearningMilestone>> GetPathMilestonesAsync(int pathId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.LearningMilestones
            .Where(m => m.LearningPathId == pathId)
            .OrderBy(m => m.OrderIndex)
            .ToListAsync();
    }

    public async Task<bool> CompleteMilestoneAsync(int milestoneId, int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var milestone = await context.LearningMilestones
            .Include(m => m.LearningPath)
            .FirstOrDefaultAsync(m => m.Id == milestoneId);
            
        if (milestone == null || milestone.IsCompleted) return false;

        milestone.IsCompleted = true;
        milestone.CompletedDate = DateTime.UtcNow;
        
        // Update learning path progress
        if (milestone.LearningPath != null)
        {
            var totalMilestones = await context.LearningMilestones
                .CountAsync(m => m.LearningPathId == milestone.LearningPathId);
            var completedMilestones = await context.LearningMilestones
                .CountAsync(m => m.LearningPathId == milestone.LearningPathId && m.IsCompleted);
                
            milestone.LearningPath.ProgressPercentage = 
                totalMilestones > 0 ? (completedMilestones * 100 / totalMilestones) : 0;
                
            if (milestone.LearningPath.ProgressPercentage >= 100)
            {
                milestone.LearningPath.CompletedDate = DateTime.UtcNow;
            }
        }

        // Award points
        var progress = await GetUserProgressAsync(userId);
        if (progress != null)
        {
            progress.TotalPoints += milestone.Points;
            progress.ExperiencePoints += milestone.Points;
            await UpdateUserProgressAsync(progress);
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<LearningPath>> GetRecommendedPathsAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var userProgress = await GetUserProgressAsync(userId);
        if (userProgress == null)
        {
            // Return beginner paths for new users
            return await context.LearningPaths
                .Where(lp => lp.DifficultyLevel == "beginner" && lp.UserId != userId)
                .Take(3)
                .ToListAsync();
        }

        // Use AI to recommend paths if available
        try
        {
            var prompt = $"Recommend learning paths for a user with: " +
                        $"Level {userProgress.Level}, " +
                        $"{userProgress.VersesRead} verses read, " +
                        $"{userProgress.LessonsCompleted} lessons completed.";
                        
            var aiResponse = await _aiService.GetCompletionAsync(prompt);
            
            if (aiResponse.Success)
            {
                _logger.LogInformation("AI recommendations generated for user {UserId}", userId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI recommendations failed, using fall-back");
        }

        // Fall-back: Rule-based recommendations
        var difficulty = userProgress.Level <= 3 ? "beginner" : 
                        userProgress.Level <= 7 ? "intermediate" : "advanced";
                        
        return await context.LearningPaths
            .Where(lp => lp.DifficultyLevel == difficulty && lp.UserId != userId && lp.IsActive)
            .Take(5)
            .ToListAsync();
    }
}
