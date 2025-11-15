using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChallengeController : ControllerBase
{
    private readonly IChallengeService _challengeService;
    private readonly ILogger<ChallengeController> _logger;

    public ChallengeController(
        IChallengeService challengeService,
        ILogger<ChallengeController> logger)
    {
        _challengeService = challengeService;
        _logger = logger;
    }

    /// <summary>
    /// Get active challenges
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Challenge>>> GetActiveChallenges()
    {
        var challenges = await _challengeService.GetActiveChallengesAsync();
        return Ok(challenges);
    }

    /// <summary>
    /// Get a specific challenge
    /// </summary>
    [HttpGet("{challengeId}")]
    public async Task<ActionResult<Challenge>> GetChallenge(int challengeId)
    {
        var challenge = await _challengeService.GetChallengeAsync(challengeId);
        if (challenge == null) return NotFound();
        return Ok(challenge);
    }

    /// <summary>
    /// Join a challenge
    /// </summary>
    [HttpPost("{challengeId}/join")]
    public async Task<ActionResult<UserChallenge>> JoinChallenge(int challengeId, [FromBody] JoinChallengeRequest request)
    {
        try
        {
            var result = await _challengeService.JoinChallengeAsync(request.UserId, challengeId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Update challenge progress
    /// </summary>
    [HttpPut("{challengeId}/progress")]
    public async Task<ActionResult<UserChallenge>> UpdateProgress(
        int challengeId, [FromBody] UpdateChallengeProgressRequest request)
    {
        try
        {
            var result = await _challengeService.UpdateChallengeProgressAsync(
                request.UserId, challengeId, request.ProgressValue);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get user challenges
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserChallenge>>> GetUserChallenges(
        int userId, [FromQuery] bool? completedOnly = null)
    {
        var challenges = await _challengeService.GetUserChallengesAsync(userId, completedOnly);
        return Ok(challenges);
    }
}

public class JoinChallengeRequest
{
    public int UserId { get; set; }
}

public class UpdateChallengeProgressRequest
{
    public int UserId { get; set; }
    public int ProgressValue { get; set; }
}
