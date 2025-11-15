using Microsoft.EntityFrameworkCore;
using AlHidayahPro.Data;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service implementation for Hadith operations
/// </summary>
public class HadithService : IHadithService
{
    private readonly IslamicDbContext _context;

    public HadithService(IslamicDbContext context)
    {
        _context = context;
    }

    public async Task<List<HadithDto>> GetHadithsByCollectionAsync(string collection, int? bookNumber = null)
    {
        var query = _context.Hadiths
            .Where(h => h.Collection.ToLower() == collection.ToLower());

        if (bookNumber.HasValue)
        {
            query = query.Where(h => h.BookNumber == bookNumber.Value);
        }

        var hadiths = await query
            .OrderBy(h => h.BookNumber)
            .ThenBy(h => h.HadithNumber)
            .Take(50) // Limit results
            .ToListAsync();

        return hadiths.Select(h => new HadithDto
        {
            Collection = h.Collection,
            Book = h.Book,
            BookNumber = h.BookNumber,
            HadithNumber = h.HadithNumber,
            ArabicText = h.ArabicText,
            EnglishText = h.EnglishText,
            Grade = h.Grade,
            Narrator = h.Narrator,
            Chapter = h.Chapter
        }).ToList();
    }

    public async Task<HadithDto?> GetHadithByNumberAsync(string collection, string hadithNumber)
    {
        var hadith = await _context.Hadiths
            .FirstOrDefaultAsync(h => 
                h.Collection.ToLower() == collection.ToLower() && 
                h.HadithNumber == hadithNumber);

        if (hadith == null) return null;

        return new HadithDto
        {
            Collection = hadith.Collection,
            Book = hadith.Book,
            BookNumber = hadith.BookNumber,
            HadithNumber = hadith.HadithNumber,
            ArabicText = hadith.ArabicText,
            EnglishText = hadith.EnglishText,
            Grade = hadith.Grade,
            Narrator = hadith.Narrator,
            Chapter = hadith.Chapter
        };
    }

    public async Task<HadithSearchResults> SearchHadithAsync(HadithSearchRequest request)
    {
        var query = _context.Hadiths.AsQueryable();

        // Apply collection filter if specified
        if (!string.IsNullOrWhiteSpace(request.Collection))
        {
            query = query.Where(h => h.Collection.ToLower() == request.Collection.ToLower());
        }

        // Apply grade filter if specified
        if (!string.IsNullOrWhiteSpace(request.Grade))
        {
            query = query.Where(h => h.Grade.ToLower() == request.Grade.ToLower());
        }

        // Apply text search
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            var searchTerm = request.Query.ToLower();
            query = query.Where(h =>
                (request.SearchEnglish && h.EnglishText.ToLower().Contains(searchTerm)) ||
                (request.SearchArabic && h.ArabicText != null && h.ArabicText.ToLower().Contains(searchTerm)) ||
                h.Book.ToLower().Contains(searchTerm) ||
                (h.Chapter != null && h.Chapter.ToLower().Contains(searchTerm))
            );
        }

        // Get total count
        var totalCount = await query.CountAsync();

        // Apply pagination
        var hadiths = await query
            .OrderBy(h => h.Collection)
            .ThenBy(h => h.BookNumber)
            .ThenBy(h => h.HadithNumber)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new HadithSearchResults
        {
            Results = hadiths.Select(h => new HadithDto
            {
                Collection = h.Collection,
                Book = h.Book,
                BookNumber = h.BookNumber,
                HadithNumber = h.HadithNumber,
                ArabicText = h.ArabicText,
                EnglishText = h.EnglishText,
                Grade = h.Grade,
                Narrator = h.Narrator,
                Chapter = h.Chapter
            }).ToList(),
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<List<string>> GetCollectionsAsync()
    {
        return await _context.Hadiths
            .Select(h => h.Collection)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }
}
