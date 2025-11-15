namespace AlHidayahPro.Api.DTOs;

/// <summary>
/// Data transfer object for Audio Recitation
/// </summary>
public class AudioRecitationDto
{
    public string ReciterName { get; set; } = string.Empty;
    public int SurahNumber { get; set; }
    public int? AyahNumber { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string Format { get; set; } = "mp3";
    public int? DurationSeconds { get; set; }
    public string? RecitationStyle { get; set; }
}

/// <summary>
/// Available reciters response
/// </summary>
public class RecitersResponse
{
    public List<string> Reciters { get; set; } = new();
}
