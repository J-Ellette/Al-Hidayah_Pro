namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents an Islamic book or text resource
/// </summary>
public class Book
{
    public int Id { get; set; }
    
    /// <summary>
    /// Reference to the content module
    /// </summary>
    public int ModuleId { get; set; }
    public ContentModule? Module { get; set; }
    
    /// <summary>
    /// Book title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Author name
    /// </summary>
    public string Author { get; set; } = string.Empty;
    
    /// <summary>
    /// Book category (e.g., "Fiqh", "Seerah", "Tafsir")
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Language code
    /// </summary>
    public string Language { get; set; } = string.Empty;
    
    /// <summary>
    /// Book description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// ISBN if available
    /// </summary>
    public string? ISBN { get; set; }
    
    /// <summary>
    /// Publisher
    /// </summary>
    public string? Publisher { get; set; }
    
    /// <summary>
    /// Publication year
    /// </summary>
    public int? PublicationYear { get; set; }
    
    /// <summary>
    /// Number of pages
    /// </summary>
    public int? PageCount { get; set; }
    
    /// <summary>
    /// Cover image URL
    /// </summary>
    public string? CoverImageUrl { get; set; }
    
    /// <summary>
    /// Collection of chapters
    /// </summary>
    public ICollection<BookChapter> Chapters { get; set; } = new List<BookChapter>();
}

/// <summary>
/// Represents a chapter within a book
/// </summary>
public class BookChapter
{
    public int Id { get; set; }
    
    /// <summary>
    /// Reference to parent book
    /// </summary>
    public int BookId { get; set; }
    public Book? Book { get; set; }
    
    /// <summary>
    /// Chapter number
    /// </summary>
    public int ChapterNumber { get; set; }
    
    /// <summary>
    /// Chapter title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Chapter content (can be HTML or Markdown)
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional summary
    /// </summary>
    public string? Summary { get; set; }
    
    /// <summary>
    /// Page number in original book
    /// </summary>
    public int? PageNumber { get; set; }
}
