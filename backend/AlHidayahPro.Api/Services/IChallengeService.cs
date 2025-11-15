using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for challenge/gamification features
/// </summary>
public interface IChallengeService
{
    Task<IEnumerable<Challenge>> GetActiveChallengesAsync();
    Task<Challenge?> GetChallengeAsync(int challengeId);
    Task<UserChallenge> JoinChallengeAsync(int userId, int challengeId);
    Task<UserChallenge> UpdateChallengeProgressAsync(int userId, int challengeId, int progressValue);
    Task<IEnumerable<UserChallenge>> GetUserChallengesAsync(int userId, bool? completedOnly = null);
}
