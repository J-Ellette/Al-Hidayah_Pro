using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for challenge/gamification features
/// </summary>
public class ChallengeService : IChallengeService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILearningService _learningService;
    private readonly ILogger<ChallengeService> _logger;

    public ChallengeService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILearningService learningService,
        ILogger<ChallengeService> logger)
    {
        _dbFactory = dbFactory;
        _learningService = learningService;
        _logger = logger;
    }

    public async Task<IEnumerable<Challenge>> GetActiveChallengesAsync()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var now = DateTime.UtcNow;
        return await context.Challenges
            .Where(c => c.IsActive && c.StartDate <= now && c.EndDate >= now)
            .OrderBy(c => c.EndDate)
            .ToListAsync();
    }

    public async Task<Challenge?> GetChallengeAsync(int challengeId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.Challenges.FindAsync(challengeId);
    }

    public async Task<UserChallenge> JoinChallengeAsync(int userId, int challengeId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        // Check if already joined
        var existing = await context.UserChallenges
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);

        if (existing != null)
        {
            return existing;
        }

        var challenge = await context.Challenges.FindAsync(challengeId);
        if (challenge == null)
        {
            throw new ArgumentException("Challenge not found", nameof(challengeId));
        }

        var userChallenge = new UserChallenge
        {
            UserId = userId,
            ChallengeId = challengeId,
            JoinedAt = DateTime.UtcNow
        };

        context.UserChallenges.Add(userChallenge);
        await context.SaveChangesAsync();

        _logger.LogInformation("User {UserId} joined challenge {ChallengeId}", userId, challengeId);

        return userChallenge;
    }

    public async Task<UserChallenge> UpdateChallengeProgressAsync(int userId, int challengeId, int progressValue)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var userChallenge = await context.UserChallenges
            .Include(uc => uc.Challenge)
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);

        if (userChallenge == null)
        {
            throw new ArgumentException("User not enrolled in this challenge");
        }

        userChallenge.CurrentValue = progressValue;
        userChallenge.LastActivityAt = DateTime.UtcNow;

        if (userChallenge.Challenge != null)
        {
            userChallenge.ProgressPercentage = userChallenge.Challenge.TargetValue > 0
                ? Math.Min(100, (userChallenge.CurrentValue * 100) / userChallenge.Challenge.TargetValue)
                : 0;

            // Check if completed
            if (userChallenge.ProgressPercentage >= 100 && !userChallenge.IsCompleted)
            {
                userChallenge.IsCompleted = true;
                userChallenge.CompletedAt = DateTime.UtcNow;

                // Award points
                var progress = await _learningService.GetUserProgressAsync(userId);
                if (progress != null)
                {
                    progress.TotalPoints += userChallenge.Challenge.RewardPoints;
                    progress.ExperiencePoints += userChallenge.Challenge.RewardPoints;
                    await _learningService.UpdateUserProgressAsync(progress);
                }

                _logger.LogInformation("User {UserId} completed challenge {ChallengeId} and earned {Points} points",
                    userId, challengeId, userChallenge.Challenge.RewardPoints);
            }
        }

        await context.SaveChangesAsync();

        return userChallenge;
    }

    public async Task<IEnumerable<UserChallenge>> GetUserChallengesAsync(int userId, bool? completedOnly = null)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        IQueryable<UserChallenge> query = context.UserChallenges
            .Where(uc => uc.UserId == userId);

        if (completedOnly.HasValue)
        {
            query = query.Where(uc => uc.IsCompleted == completedOnly.Value);
        }

        return await query
            .Include(uc => uc.Challenge)
            .OrderByDescending(uc => uc.IsCompleted)
            .ThenByDescending(uc => uc.LastActivityAt ?? uc.JoinedAt)
            .ToListAsync();
    }
}
