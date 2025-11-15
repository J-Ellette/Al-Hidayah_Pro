using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for smart navigation features
/// </summary>
public interface INavigationService
{
    // Reading History
    Task<ReadingHistory> SaveReadingPositionAsync(ReadingHistory history);
    Task<ReadingHistory?> GetReadingPositionAsync(int userId, string contentType, int contentId);
    Task<IEnumerable<ReadingHistory>> GetRecentReadingHistoryAsync(int userId, int limit = 10);
    
    // Bookmarks
    Task<Bookmark> CreateBookmarkAsync(Bookmark bookmark);
    Task<IEnumerable<Bookmark>> GetUserBookmarksAsync(int userId, string? contentType = null);
    Task<IEnumerable<Bookmark>> GetBookmarksByTagAsync(int userId, string tag);
    Task<bool> DeleteBookmarkAsync(int bookmarkId);
}
