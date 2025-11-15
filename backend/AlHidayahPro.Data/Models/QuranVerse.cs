namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a verse from the Quran
/// </summary>
public class QuranVerse
{
    public int Id { get; set; }
    
    /// <summary>
    /// Surah number (1-114)
    /// </summary>
    public int SurahNumber { get; set; }
    
    /// <summary>
    /// Ayah (verse) number within the surah
    /// </summary>
    public int AyahNumber { get; set; }
    
    /// <summary>
    /// Arabic text of the verse
    /// </summary>
    public string ArabicText { get; set; } = string.Empty;
    
    /// <summary>
    /// English translation
    /// </summary>
    public string? EnglishTranslation { get; set; }
    
    /// <summary>
    /// Transliteration of the Arabic text
    /// </summary>
    public string? Transliteration { get; set; }
    
    /// <summary>
    /// Audio recitation URL
    /// </summary>
    public string? AudioUrl { get; set; }
    
    /// <summary>
    /// Juz (part) number
    /// </summary>
    public int JuzNumber { get; set; }
    
    /// <summary>
    /// Page number in standard Mushaf
    /// </summary>
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Navigation property to Surah
    /// </summary>
    public Surah? Surah { get; set; }
}
