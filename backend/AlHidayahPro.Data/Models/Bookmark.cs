namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a user bookmark with tags
/// </summary>
public class Bookmark
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Title of the bookmark
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Content type (quran_verse, hadith, page, article)
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Content ID
    /// </summary>
    public int ContentId { get; set; }
    
    /// <summary>
    /// Reference text (e.g., "Surah 2:255", "Bukhari 1")
    /// </summary>
    public string Reference { get; set; } = string.Empty;
    
    /// <summary>
    /// Comma-separated tags
    /// </summary>
    public string? Tags { get; set; }
    
    /// <summary>
    /// Optional notes
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Created timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Is this a favorite bookmark?
    /// </summary>
    public bool IsFavorite { get; set; } = false;
    
    /// <summary>
    /// Color label (for organization)
    /// </summary>
    public string? ColorLabel { get; set; }
    
    // Navigation property
    public User? User { get; set; }
}
