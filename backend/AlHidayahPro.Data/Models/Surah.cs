namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a Surah (chapter) of the Quran
/// </summary>
public class Surah
{
    public int Id { get; set; }
    
    /// <summary>
    /// Surah number (1-114)
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Arabic name of the surah
    /// </summary>
    public string ArabicName { get; set; } = string.Empty;
    
    /// <summary>
    /// English transliteration of the name
    /// </summary>
    public string EnglishName { get; set; } = string.Empty;
    
    /// <summary>
    /// English translation of the name
    /// </summary>
    public string? EnglishTranslation { get; set; }
    
    /// <summary>
    /// Total number of verses in the surah
    /// </summary>
    public int NumberOfAyahs { get; set; }
    
    /// <summary>
    /// Revelation location (Meccan or Medinan)
    /// </summary>
    public string RevelationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Collection of verses in this surah
    /// </summary>
    public ICollection<QuranVerse> Verses { get; set; } = new List<QuranVerse>();
}
