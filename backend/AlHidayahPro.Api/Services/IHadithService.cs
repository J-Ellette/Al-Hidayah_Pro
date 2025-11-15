using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for Hadith operations
/// </summary>
public interface IHadithService
{
    Task<List<HadithDto>> GetHadithsByCollectionAsync(string collection, int? bookNumber = null);
    Task<HadithDto?> GetHadithByNumberAsync(string collection, string hadithNumber);
    Task<HadithSearchResults> SearchHadithAsync(HadithSearchRequest request);
    Task<List<string>> GetCollectionsAsync();
}
