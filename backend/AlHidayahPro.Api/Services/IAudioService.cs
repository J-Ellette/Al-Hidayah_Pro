using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for Audio Recitation operations
/// </summary>
public interface IAudioService
{
    Task<AudioRecitationDto?> GetRecitationAsync(string reciterName, int surahNumber, int? ayahNumber = null);
    Task<List<AudioRecitationDto>> GetRecitationsBySurahAsync(string reciterName, int surahNumber);
    Task<List<string>> GetRecitersAsync();
}
