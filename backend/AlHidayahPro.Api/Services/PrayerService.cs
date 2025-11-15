using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for prayer tracking and worship tools
/// </summary>
public class PrayerService : IPrayerService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILogger<PrayerService> _logger;

    public PrayerService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILogger<PrayerService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    public async Task<PrayerLog> LogPrayerAsync(PrayerLog log)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        // Check if prayer already logged for this day
        var existing = await context.PrayerLogs
            .FirstOrDefaultAsync(p => 
                p.UserId == log.UserId && 
                p.PrayerName == log.PrayerName && 
                p.PrayerDate.Date == log.PrayerDate.Date);

        if (existing != null)
        {
            // Update existing log
            existing.OnTime = log.OnTime;
            existing.InCongregation = log.InCongregation;
            existing.Location = log.Location;
            existing.Notes = log.Notes;
            existing.LoggedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return existing;
        }

        context.PrayerLogs.Add(log);
        await context.SaveChangesAsync();

        _logger.LogInformation("Prayer {PrayerName} logged for user {UserId} on {Date}", 
            log.PrayerName, log.UserId, log.PrayerDate.Date);

        return log;
    }

    public async Task<IEnumerable<PrayerLog>> GetUserPrayerLogsAsync(int userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var query = context.PrayerLogs.Where(p => p.UserId == userId);

        if (startDate.HasValue)
        {
            query = query.Where(p => p.PrayerDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(p => p.PrayerDate <= endDate.Value);
        }

        return await query
            .OrderByDescending(p => p.PrayerDate)
            .ThenBy(p => p.PrayerName)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetPrayerStreakAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var logs = await context.PrayerLogs
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.PrayerDate)
            .ToListAsync();

        var currentStreak = 0;
        var longestStreak = 0;
        var tempStreak = 0;
        var lastDate = DateTime.Today;

        // Group by date
        var dailyCounts = logs
            .GroupBy(p => p.PrayerDate.Date)
            .OrderByDescending(g => g.Key)
            .ToList();

        foreach (var day in dailyCounts)
        {
            var prayerCount = day.Count();
            
            // Check if this day has all 5 prayers
            if (prayerCount >= 5)
            {
                if (day.Key == lastDate || day.Key == lastDate.AddDays(-1))
                {
                    tempStreak++;
                    lastDate = day.Key;
                }
                else
                {
                    break; // Streak broken
                }
            }
            else
            {
                break; // Streak broken
            }
        }

        currentStreak = tempStreak;

        // Calculate longest streak
        tempStreak = 0;
        lastDate = dailyCounts.FirstOrDefault()?.Key ?? DateTime.Today;

        foreach (var day in dailyCounts)
        {
            if (day.Count() >= 5)
            {
                if (day.Key == lastDate || day.Key == lastDate.AddDays(-1))
                {
                    tempStreak++;
                    longestStreak = Math.Max(longestStreak, tempStreak);
                    lastDate = day.Key;
                }
                else
                {
                    tempStreak = 1;
                    lastDate = day.Key;
                }
            }
        }

        return new Dictionary<string, int>
        {
            { "currentStreak", currentStreak },
            { "longestStreak", longestStreak },
            { "totalPrayers", logs.Count }
        };
    }

    public async Task<IEnumerable<ReadingGoal>> GetUserReadingGoalsAsync(int userId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        return await context.ReadingGoals
            .Where(g => g.UserId == userId)
            .OrderByDescending(g => g.IsActive)
            .ThenByDescending(g => g.StartDate)
            .ToListAsync();
    }

    public async Task<ReadingGoal> CreateReadingGoalAsync(ReadingGoal goal)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        context.ReadingGoals.Add(goal);
        await context.SaveChangesAsync();

        _logger.LogInformation("Reading goal created for user {UserId}: {Title}", 
            goal.UserId, goal.Title);

        return goal;
    }

    public async Task<ReadingGoal> UpdateReadingGoalProgressAsync(int goalId, int progressValue)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var goal = await context.ReadingGoals.FindAsync(goalId);
        if (goal == null)
        {
            throw new ArgumentException("Goal not found", nameof(goalId));
        }

        goal.CurrentValue = progressValue;
        goal.ProgressPercentage = goal.TargetValue > 0 
            ? Math.Min(100, (goal.CurrentValue * 100) / goal.TargetValue) 
            : 0;

        if (goal.ProgressPercentage >= 100 && !goal.CompletedDate.HasValue)
        {
            goal.CompletedDate = DateTime.UtcNow;
            goal.IsActive = false;
        }

        await context.SaveChangesAsync();

        return goal;
    }

    public async Task<IEnumerable<DailyDhikr>> GetDhikrByCategoryAsync(string category)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        return await context.DailyDhikrs
            .Where(d => d.Category == category)
            .OrderBy(d => d.OrderIndex)
            .ToListAsync();
    }

    public async Task<DailyDhikr?> GetRandomDailyDhikrAsync()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var count = await context.DailyDhikrs.CountAsync();
        if (count == 0) return null;

        var skip = new Random().Next(0, count);
        return await context.DailyDhikrs
            .Skip(skip)
            .FirstOrDefaultAsync();
    }
}
