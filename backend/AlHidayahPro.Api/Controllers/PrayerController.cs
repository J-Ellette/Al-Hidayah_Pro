using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrayerController : ControllerBase
{
    private readonly IPrayerService _prayerService;
    private readonly ILogger<PrayerController> _logger;

    public PrayerController(
        IPrayerService prayerService,
        ILogger<PrayerController> logger)
    {
        _prayerService = prayerService;
        _logger = logger;
    }

    /// <summary>
    /// Log a prayer
    /// </summary>
    [HttpPost("log")]
    public async Task<ActionResult<PrayerLog>> LogPrayer([FromBody] PrayerLog log)
    {
        var result = await _prayerService.LogPrayerAsync(log);
        return Ok(result);
    }

    /// <summary>
    /// Get prayer logs for a user
    /// </summary>
    [HttpGet("logs/{userId}")]
    public async Task<ActionResult<IEnumerable<PrayerLog>>> GetPrayerLogs(
        int userId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var logs = await _prayerService.GetUserPrayerLogsAsync(userId, startDate, endDate);
        return Ok(logs);
    }

    /// <summary>
    /// Get prayer streak statistics
    /// </summary>
    [HttpGet("streak/{userId}")]
    public async Task<ActionResult<Dictionary<string, int>>> GetStreak(int userId)
    {
        var streak = await _prayerService.GetPrayerStreakAsync(userId);
        return Ok(streak);
    }

    /// <summary>
    /// Get reading goals
    /// </summary>
    [HttpGet("goals/{userId}")]
    public async Task<ActionResult<IEnumerable<ReadingGoal>>> GetReadingGoals(int userId)
    {
        var goals = await _prayerService.GetUserReadingGoalsAsync(userId);
        return Ok(goals);
    }

    /// <summary>
    /// Create a reading goal
    /// </summary>
    [HttpPost("goals")]
    public async Task<ActionResult<ReadingGoal>> CreateReadingGoal([FromBody] ReadingGoal goal)
    {
        var result = await _prayerService.CreateReadingGoalAsync(goal);
        return CreatedAtAction(nameof(GetReadingGoals), new { userId = result.UserId }, result);
    }

    /// <summary>
    /// Update reading goal progress
    /// </summary>
    [HttpPut("goals/{goalId}/progress")]
    public async Task<ActionResult<ReadingGoal>> UpdateProgress(int goalId, [FromBody] UpdateProgressRequest request)
    {
        try
        {
            var result = await _prayerService.UpdateReadingGoalProgressAsync(goalId, request.ProgressValue);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get dhikr by category
    /// </summary>
    [HttpGet("dhikr")]
    public async Task<ActionResult<IEnumerable<DailyDhikr>>> GetDhikr([FromQuery] string category)
    {
        var dhikr = await _prayerService.GetDhikrByCategoryAsync(category);
        return Ok(dhikr);
    }

    /// <summary>
    /// Get random daily dhikr
    /// </summary>
    [HttpGet("dhikr/daily")]
    public async Task<ActionResult<DailyDhikr>> GetDailyDhikr()
    {
        var dhikr = await _prayerService.GetRandomDailyDhikrAsync();
        if (dhikr == null) return NotFound("No dhikr available");
        return Ok(dhikr);
    }
}

public class UpdateProgressRequest
{
    public int ProgressValue { get; set; }
}
