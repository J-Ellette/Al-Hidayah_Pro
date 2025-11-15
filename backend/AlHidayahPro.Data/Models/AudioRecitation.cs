namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents an audio recitation of the Quran
/// </summary>
public class AudioRecitation
{
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the reciter
    /// </summary>
    public string ReciterName { get; set; } = string.Empty;
    
    /// <summary>
    /// Surah number
    /// </summary>
    public int SurahNumber { get; set; }
    
    /// <summary>
    /// Ayah number (null for full surah)
    /// </summary>
    public int? AyahNumber { get; set; }
    
    /// <summary>
    /// Audio file URL or path
    /// </summary>
    public string AudioUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Audio format (mp3, ogg, etc.)
    /// </summary>
    public string Format { get; set; } = "mp3";
    
    /// <summary>
    /// Duration in seconds
    /// </summary>
    public int? DurationSeconds { get; set; }
    
    /// <summary>
    /// Recitation style (Murattal, Mujawwad, etc.)
    /// </summary>
    public string? RecitationStyle { get; set; }
}
