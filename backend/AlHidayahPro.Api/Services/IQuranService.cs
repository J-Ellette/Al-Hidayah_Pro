using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for Quran operations
/// </summary>
public interface IQuranService
{
    Task<SurahDto?> GetSurahAsync(int surahNumber);
    Task<List<SurahDto>> GetAllSurahsAsync();
    Task<QuranVerseDto?> GetVerseAsync(int surahNumber, int ayahNumber);
    Task<List<QuranVerseDto>> GetVersesBySurahAsync(int surahNumber);
    Task<QuranSearchResults> SearchVersesAsync(QuranSearchRequest request);
}
