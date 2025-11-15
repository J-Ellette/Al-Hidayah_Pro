using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for prayer tracking and worship tools
/// </summary>
public interface IPrayerService
{
    // Prayer Logging
    Task<PrayerLog> LogPrayerAsync(PrayerLog log);
    Task<IEnumerable<PrayerLog>> GetUserPrayerLogsAsync(int userId, DateTime? startDate = null, DateTime? endDate = null);
    Task<Dictionary<string, int>> GetPrayerStreakAsync(int userId);
    
    // Reading Goals
    Task<IEnumerable<ReadingGoal>> GetUserReadingGoalsAsync(int userId);
    Task<ReadingGoal> CreateReadingGoalAsync(ReadingGoal goal);
    Task<ReadingGoal> UpdateReadingGoalProgressAsync(int goalId, int progressValue);
    
    // Daily Dhikr
    Task<IEnumerable<DailyDhikr>> GetDhikrByCategoryAsync(string category);
    Task<DailyDhikr?> GetRandomDailyDhikrAsync();
}
