using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for learning features
/// </summary>
public interface ILearningService
{
    // Learning Paths
    Task<IEnumerable<LearningPath>> GetUserLearningPathsAsync(int userId);
    Task<LearningPath?> GetLearningPathAsync(int pathId);
    Task<LearningPath> CreateLearningPathAsync(LearningPath path);
    Task<LearningPath> UpdateLearningPathAsync(LearningPath path);
    Task<bool> DeleteLearningPathAsync(int pathId);
    
    // Progress
    Task<UserProgress?> GetUserProgressAsync(int userId);
    Task UpdateUserProgressAsync(UserProgress progress);
    Task IncrementProgressMetricAsync(int userId, string metricType, int amount = 1);
    
    // Achievements
    Task<IEnumerable<Achievement>> GetAllAchievementsAsync();
    Task<IEnumerable<UserAchievement>> GetUserAchievementsAsync(int userId);
    Task<UserAchievement?> AwardAchievementAsync(int userId, string achievementId);
    
    // Milestones
    Task<IEnumerable<LearningMilestone>> GetPathMilestonesAsync(int pathId);
    Task<bool> CompleteMilestoneAsync(int milestoneId, int userId);
    
    // AI-powered recommendations
    Task<IEnumerable<LearningPath>> GetRecommendedPathsAsync(int userId);
}
