namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a Quran verse translation in a specific language
/// </summary>
public class QuranTranslation
{
    public int Id { get; set; }
    
    /// <summary>
    /// Reference to the content module
    /// </summary>
    public int ModuleId { get; set; }
    public ContentModule? Module { get; set; }
    
    /// <summary>
    /// Surah number (1-114)
    /// </summary>
    public int SurahNumber { get; set; }
    
    /// <summary>
    /// Ayah number within the Surah
    /// </summary>
    public int AyahNumber { get; set; }
    
    /// <summary>
    /// Translated text
    /// </summary>
    public string Translation { get; set; } = string.Empty;
    
    /// <summary>
    /// Language code (e.g., "en", "ur", "fr")
    /// </summary>
    public string Language { get; set; } = string.Empty;
    
    /// <summary>
    /// Translator name
    /// </summary>
    public string? Translator { get; set; }
    
    /// <summary>
    /// Optional footnotes or commentary
    /// </summary>
    public string? Footnotes { get; set; }
}
