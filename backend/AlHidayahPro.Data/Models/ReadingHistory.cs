namespace AlHidayahPro.Data.Models;

/// <summary>
/// Tracks user's reading position to allow resuming
/// </summary>
public class ReadingHistory
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Content type (quran, hadith, book)
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Content ID (surah number, hadith ID, book ID)
    /// </summary>
    public int ContentId { get; set; }
    
    /// <summary>
    /// Position within content (verse number, page number, etc.)
    /// </summary>
    public int Position { get; set; }
    
    /// <summary>
    /// Last read timestamp
    /// </summary>
    public DateTime LastReadAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;
    
    /// <summary>
    /// Additional metadata (JSON)
    /// </summary>
    public string? Metadata { get; set; }
    
    // Navigation property
    public User? User { get; set; }
}
