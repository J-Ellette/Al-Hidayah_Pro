using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NavigationController : ControllerBase
{
    private readonly INavigationService _navigationService;
    private readonly ILogger<NavigationController> _logger;

    public NavigationController(
        INavigationService navigationService,
        ILogger<NavigationController> logger)
    {
        _navigationService = navigationService;
        _logger = logger;
    }

    /// <summary>
    /// Save reading position
    /// </summary>
    [HttpPost("history")]
    public async Task<ActionResult<ReadingHistory>> SaveReadingPosition([FromBody] ReadingHistory history)
    {
        var result = await _navigationService.SaveReadingPositionAsync(history);
        return Ok(result);
    }

    /// <summary>
    /// Get reading position for specific content
    /// </summary>
    [HttpGet("history/{userId}/{contentType}/{contentId}")]
    public async Task<ActionResult<ReadingHistory>> GetReadingPosition(
        int userId, string contentType, int contentId)
    {
        var result = await _navigationService.GetReadingPositionAsync(userId, contentType, contentId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get recent reading history
    /// </summary>
    [HttpGet("history/{userId}/recent")]
    public async Task<ActionResult<IEnumerable<ReadingHistory>>> GetRecentHistory(
        int userId, [FromQuery] int limit = 10)
    {
        var result = await _navigationService.GetRecentReadingHistoryAsync(userId, limit);
        return Ok(result);
    }

    /// <summary>
    /// Create a bookmark
    /// </summary>
    [HttpPost("bookmarks")]
    public async Task<ActionResult<Bookmark>> CreateBookmark([FromBody] Bookmark bookmark)
    {
        var result = await _navigationService.CreateBookmarkAsync(bookmark);
        return CreatedAtAction(nameof(GetUserBookmarks), new { userId = result.UserId }, result);
    }

    /// <summary>
    /// Get user bookmarks
    /// </summary>
    [HttpGet("bookmarks/{userId}")]
    public async Task<ActionResult<IEnumerable<Bookmark>>> GetUserBookmarks(
        int userId, [FromQuery] string? contentType = null)
    {
        var result = await _navigationService.GetUserBookmarksAsync(userId, contentType);
        return Ok(result);
    }

    /// <summary>
    /// Get bookmarks by tag
    /// </summary>
    [HttpGet("bookmarks/{userId}/tag/{tag}")]
    public async Task<ActionResult<IEnumerable<Bookmark>>> GetBookmarksByTag(int userId, string tag)
    {
        var result = await _navigationService.GetBookmarksByTagAsync(userId, tag);
        return Ok(result);
    }

    /// <summary>
    /// Delete a bookmark
    /// </summary>
    [HttpDelete("bookmarks/{bookmarkId}")]
    public async Task<ActionResult> DeleteBookmark(int bookmarkId)
    {
        var result = await _navigationService.DeleteBookmarkAsync(bookmarkId);
        if (!result) return NotFound();
        return NoContent();
    }
}
