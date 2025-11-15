namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a daily dhikr (remembrance) or dua
/// </summary>
public class DailyDhikr
{
    public int Id { get; set; }
    
    /// <summary>
    /// Title of the dhikr
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Arabic text
    /// </summary>
    public string ArabicText { get; set; } = string.Empty;
    
    /// <summary>
    /// Transliteration
    /// </summary>
    public string? Transliteration { get; set; }
    
    /// <summary>
    /// English translation
    /// </summary>
    public string Translation { get; set; } = string.Empty;
    
    /// <summary>
    /// Category (morning, evening, sleeping, eating, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Reference (Quran verse, hadith reference)
    /// </summary>
    public string? Reference { get; set; }
    
    /// <summary>
    /// Number of times to recite
    /// </summary>
    public int RepetitionCount { get; set; } = 1;
    
    /// <summary>
    /// Order/sequence in the category
    /// </summary>
    public int OrderIndex { get; set; }
    
    /// <summary>
    /// Benefits or virtues of this dhikr
    /// </summary>
    public string? Benefits { get; set; }
}
