namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a Hadith (saying/action of Prophet Muhammad PBUH)
/// </summary>
public class Hadith
{
    public int Id { get; set; }
    
    /// <summary>
    /// Collection name (e.g., Sahih Bukhari, Sahih Muslim)
    /// </summary>
    public string Collection { get; set; } = string.Empty;
    
    /// <summary>
    /// Book name within the collection
    /// </summary>
    public string Book { get; set; } = string.Empty;
    
    /// <summary>
    /// Book number
    /// </summary>
    public int BookNumber { get; set; }
    
    /// <summary>
    /// Hadith number
    /// </summary>
    public string HadithNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Arabic text of the hadith
    /// </summary>
    public string? ArabicText { get; set; }
    
    /// <summary>
    /// English translation
    /// </summary>
    public string EnglishText { get; set; } = string.Empty;
    
    /// <summary>
    /// Authenticity grade (e.g., Sahih, Hasan, Da'if)
    /// </summary>
    public string Grade { get; set; } = string.Empty;
    
    /// <summary>
    /// Chain of narrators (Isnad)
    /// </summary>
    public string? Narrator { get; set; }
    
    /// <summary>
    /// Chapter or section
    /// </summary>
    public string? Chapter { get; set; }
}
