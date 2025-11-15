using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningController : ControllerBase
{
    private readonly ILearningService _learningService;
    private readonly ILogger<LearningController> _logger;

    public LearningController(
        ILearningService learningService,
        ILogger<LearningController> logger)
    {
        _learningService = learningService;
        _logger = logger;
    }

    /// <summary>
    /// Get all learning paths for a user
    /// </summary>
    [HttpGet("paths/user/{userId}")]
    public async Task<ActionResult<IEnumerable<LearningPath>>> GetUserPaths(int userId)
    {
        var paths = await _learningService.GetUserLearningPathsAsync(userId);
        return Ok(paths);
    }

    /// <summary>
    /// Get a specific learning path
    /// </summary>
    [HttpGet("paths/{pathId}")]
    public async Task<ActionResult<LearningPath>> GetPath(int pathId)
    {
        var path = await _learningService.GetLearningPathAsync(pathId);
        if (path == null) return NotFound();
        return Ok(path);
    }

    /// <summary>
    /// Create a new learning path
    /// </summary>
    [HttpPost("paths")]
    public async Task<ActionResult<LearningPath>> CreatePath([FromBody] LearningPath path)
    {
        var created = await _learningService.CreateLearningPathAsync(path);
        return CreatedAtAction(nameof(GetPath), new { pathId = created.Id }, created);
    }

    /// <summary>
    /// Update a learning path
    /// </summary>
    [HttpPut("paths/{pathId}")]
    public async Task<ActionResult<LearningPath>> UpdatePath(int pathId, [FromBody] LearningPath path)
    {
        if (pathId != path.Id) return BadRequest();
        var updated = await _learningService.UpdateLearningPathAsync(path);
        return Ok(updated);
    }

    /// <summary>
    /// Delete a learning path
    /// </summary>
    [HttpDelete("paths/{pathId}")]
    public async Task<ActionResult> DeletePath(int pathId)
    {
        var result = await _learningService.DeleteLearningPathAsync(pathId);
        if (!result) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Get user progress
    /// </summary>
    [HttpGet("progress/{userId}")]
    public async Task<ActionResult<UserProgress>> GetProgress(int userId)
    {
        var progress = await _learningService.GetUserProgressAsync(userId);
        if (progress == null) return NotFound();
        return Ok(progress);
    }

    /// <summary>
    /// Get all achievements
    /// </summary>
    [HttpGet("achievements")]
    public async Task<ActionResult<IEnumerable<Achievement>>> GetAchievements()
    {
        var achievements = await _learningService.GetAllAchievementsAsync();
        return Ok(achievements);
    }

    /// <summary>
    /// Get user achievements
    /// </summary>
    [HttpGet("achievements/user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserAchievement>>> GetUserAchievements(int userId)
    {
        var achievements = await _learningService.GetUserAchievementsAsync(userId);
        return Ok(achievements);
    }

    /// <summary>
    /// Get milestones for a learning path
    /// </summary>
    [HttpGet("paths/{pathId}/milestones")]
    public async Task<ActionResult<IEnumerable<LearningMilestone>>> GetMilestones(int pathId)
    {
        var milestones = await _learningService.GetPathMilestonesAsync(pathId);
        return Ok(milestones);
    }

    /// <summary>
    /// Complete a milestone
    /// </summary>
    [HttpPost("milestones/{milestoneId}/complete")]
    public async Task<ActionResult> CompleteMilestone(int milestoneId, [FromBody] CompleteMilestoneRequest request)
    {
        var result = await _learningService.CompleteMilestoneAsync(milestoneId, request.UserId);
        if (!result) return NotFound();
        return Ok();
    }

    /// <summary>
    /// Get recommended learning paths for a user
    /// </summary>
    [HttpGet("paths/recommendations/{userId}")]
    public async Task<ActionResult<IEnumerable<LearningPath>>> GetRecommendations(int userId)
    {
        var recommendations = await _learningService.GetRecommendedPathsAsync(userId);
        return Ok(recommendations);
    }
}

public class CompleteMilestoneRequest
{
    public int UserId { get; set; }
}
