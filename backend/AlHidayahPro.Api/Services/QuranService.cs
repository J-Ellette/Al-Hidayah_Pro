using Microsoft.EntityFrameworkCore;
using AlHidayahPro.Data;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service implementation for Quran operations
/// </summary>
public class QuranService : IQuranService
{
    private readonly IslamicDbContext _context;

    public QuranService(IslamicDbContext context)
    {
        _context = context;
    }

    public async Task<SurahDto?> GetSurahAsync(int surahNumber)
    {
        var surah = await _context.Surahs
            .FirstOrDefaultAsync(s => s.Number == surahNumber);

        if (surah == null) return null;

        return new SurahDto
        {
            Number = surah.Number,
            ArabicName = surah.ArabicName,
            EnglishName = surah.EnglishName,
            EnglishTranslation = surah.EnglishTranslation,
            NumberOfAyahs = surah.NumberOfAyahs,
            RevelationType = surah.RevelationType
        };
    }

    public async Task<List<SurahDto>> GetAllSurahsAsync()
    {
        var surahs = await _context.Surahs
            .OrderBy(s => s.Number)
            .ToListAsync();

        return surahs.Select(s => new SurahDto
        {
            Number = s.Number,
            ArabicName = s.ArabicName,
            EnglishName = s.EnglishName,
            EnglishTranslation = s.EnglishTranslation,
            NumberOfAyahs = s.NumberOfAyahs,
            RevelationType = s.RevelationType
        }).ToList();
    }

    public async Task<QuranVerseDto?> GetVerseAsync(int surahNumber, int ayahNumber)
    {
        var verse = await _context.QuranVerses
            .FirstOrDefaultAsync(v => v.SurahNumber == surahNumber && v.AyahNumber == ayahNumber);

        if (verse == null) return null;

        return new QuranVerseDto
        {
            SurahNumber = verse.SurahNumber,
            AyahNumber = verse.AyahNumber,
            ArabicText = verse.ArabicText,
            EnglishTranslation = verse.EnglishTranslation,
            Transliteration = verse.Transliteration,
            AudioUrl = verse.AudioUrl,
            JuzNumber = verse.JuzNumber,
            PageNumber = verse.PageNumber
        };
    }

    public async Task<List<QuranVerseDto>> GetVersesBySurahAsync(int surahNumber)
    {
        var verses = await _context.QuranVerses
            .Where(v => v.SurahNumber == surahNumber)
            .OrderBy(v => v.AyahNumber)
            .ToListAsync();

        return verses.Select(v => new QuranVerseDto
        {
            SurahNumber = v.SurahNumber,
            AyahNumber = v.AyahNumber,
            ArabicText = v.ArabicText,
            EnglishTranslation = v.EnglishTranslation,
            Transliteration = v.Transliteration,
            AudioUrl = v.AudioUrl,
            JuzNumber = v.JuzNumber,
            PageNumber = v.PageNumber
        }).ToList();
    }

    public async Task<QuranSearchResults> SearchVersesAsync(QuranSearchRequest request)
    {
        var query = _context.QuranVerses.AsQueryable();

        // Apply surah filter if specified
        if (request.SurahNumber.HasValue)
        {
            query = query.Where(v => v.SurahNumber == request.SurahNumber.Value);
        }

        // Apply text search
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            var searchTerm = request.Query.ToLower();
            query = query.Where(v =>
                (request.SearchArabic && v.ArabicText.ToLower().Contains(searchTerm)) ||
                (request.SearchTranslation && v.EnglishTranslation != null && v.EnglishTranslation.ToLower().Contains(searchTerm))
            );
        }

        // Get total count
        var totalCount = await query.CountAsync();

        // Apply pagination
        var verses = await query
            .OrderBy(v => v.SurahNumber)
            .ThenBy(v => v.AyahNumber)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new QuranSearchResults
        {
            Results = verses.Select(v => new QuranVerseDto
            {
                SurahNumber = v.SurahNumber,
                AyahNumber = v.AyahNumber,
                ArabicText = v.ArabicText,
                EnglishTranslation = v.EnglishTranslation,
                Transliteration = v.Transliteration,
                AudioUrl = v.AudioUrl,
                JuzNumber = v.JuzNumber,
                PageNumber = v.PageNumber
            }).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
