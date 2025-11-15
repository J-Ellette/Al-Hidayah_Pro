using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for smart navigation features
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILogger<NavigationService> _logger;

    public NavigationService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILogger<NavigationService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    public async Task<ReadingHistory> SaveReadingPositionAsync(ReadingHistory history)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var existing = await context.ReadingHistories
            .FirstOrDefaultAsync(h => 
                h.UserId == history.UserId && 
                h.ContentType == history.ContentType && 
                h.ContentId == history.ContentId);

        if (existing != null)
        {
            existing.Position = history.Position;
            existing.ProgressPercentage = history.ProgressPercentage;
            existing.LastReadAt = DateTime.UtcNow;
            existing.Metadata = history.Metadata;
            await context.SaveChangesAsync();
            return existing;
        }

        context.ReadingHistories.Add(history);
        await context.SaveChangesAsync();

        _logger.LogInformation("Reading position saved for user {UserId}: {ContentType} {ContentId} at position {Position}",
            history.UserId, history.ContentType, history.ContentId, history.Position);

        return history;
    }

    public async Task<ReadingHistory?> GetReadingPositionAsync(int userId, string contentType, int contentId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        return await context.ReadingHistories
            .FirstOrDefaultAsync(h => 
                h.UserId == userId && 
                h.ContentType == contentType && 
                h.ContentId == contentId);
    }

    public async Task<IEnumerable<ReadingHistory>> GetRecentReadingHistoryAsync(int userId, int limit = 10)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        return await context.ReadingHistories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.LastReadAt)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<Bookmark> CreateBookmarkAsync(Bookmark bookmark)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        context.Bookmarks.Add(bookmark);
        await context.SaveChangesAsync();

        _logger.LogInformation("Bookmark created for user {UserId}: {Title}",
            bookmark.UserId, bookmark.Title);

        return bookmark;
    }

    public async Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId, string? contentType = null)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var query = context.Bookmarks.Where(b => b.UserId == userId);

        if (!string.IsNullOrEmpty(contentType))
        {
            query = query.Where(b => b.ContentType == contentType);
        }

        return await query
            .OrderByDescending(b => b.IsFavorite)
            .ThenByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Bookmark>> GetBookmarksByTagAsync(int userId, string tag)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        return await context.Bookmarks
            .Where(b => b.UserId == userId && b.Tags != null && b.Tags.Contains(tag))
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> DeleteBookmarkAsync(int bookmarkId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var bookmark = await context.Bookmarks.FindAsync(bookmarkId);
        if (bookmark == null) return false;

        context.Bookmarks.Remove(bookmark);
        await context.SaveChangesAsync();

        return true;
    }
}
