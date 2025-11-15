namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents an installable content module (Quran translations, Hadith collections, Books)
/// </summary>
public class ContentModule
{
    public int Id { get; set; }
    
    /// <summary>
    /// Unique identifier for the module (e.g., "quran-translation-sahih-international")
    /// </summary>
    public string ModuleId { get; set; } = string.Empty;
    
    /// <summary>
    /// Display name of the module
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Type of content: QuranTranslation, HadithCollection, Book, Tafsir, Audio
    /// </summary>
    public ModuleType Type { get; set; }
    
    /// <summary>
    /// Language code (e.g., "en", "ar", "ur")
    /// </summary>
    public string Language { get; set; } = string.Empty;
    
    /// <summary>
    /// Author or translator name
    /// </summary>
    public string? Author { get; set; }
    
    /// <summary>
    /// Description of the module
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Version of the module (e.g., "1.0.0")
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Source URL where the module can be downloaded
    /// </summary>
    public string? SourceUrl { get; set; }
    
    /// <summary>
    /// Whether the module is currently installed
    /// </summary>
    public bool IsInstalled { get; set; }
    
    /// <summary>
    /// Installation date
    /// </summary>
    public DateTime? InstalledDate { get; set; }
    
    /// <summary>
    /// File size in bytes
    /// </summary>
    public long FileSize { get; set; }
    
    /// <summary>
    /// License information
    /// </summary>
    public string? License { get; set; }
    
    /// <summary>
    /// Additional metadata as JSON
    /// </summary>
    public string? Metadata { get; set; }
    
    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Last update date
    /// </summary>
    public DateTime? UpdatedDate { get; set; }
}

public enum ModuleType
{
    QuranTranslation = 1,
    HadithCollection = 2,
    Book = 3,
    Tafsir = 4,
    Audio = 5,
    Commentary = 6,
    Dictionary = 7
}
