using System.Text.Json;
using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Data.Data;

/// <summary>
/// Service for importing full Quran data from JSON files
/// </summary>
public class QuranDataImporter
{
    private readonly IslamicDbContext _context;

    public QuranDataImporter(IslamicDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Import full Quran data from a JSON file
    /// Expected format: Array of Surah objects with verses
    /// </summary>
    public async Task<ImportResult> ImportQuranDataAsync(string jsonFilePath)
    {
        var result = new ImportResult();

        try
        {
            if (!File.Exists(jsonFilePath))
            {
                result.Success = false;
                result.Message = $"File not found: {jsonFilePath}";
                return result;
            }

            var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
            var quranData = JsonSerializer.Deserialize<List<SurahImportData>>(jsonContent);

            if (quranData == null || !quranData.Any())
            {
                result.Success = false;
                result.Message = "No data found in JSON file";
                return result;
            }

            // Clear existing data if reimporting
            var existingVerses = _context.QuranVerses.ToList();
            var existingSurahs = _context.Surahs.ToList();
            _context.QuranVerses.RemoveRange(existingVerses);
            _context.Surahs.RemoveRange(existingSurahs);
            await _context.SaveChangesAsync();

            foreach (var surahData in quranData)
            {
                var surah = new Surah
                {
                    Number = surahData.Number,
                    ArabicName = surahData.ArabicName,
                    EnglishName = surahData.EnglishName,
                    EnglishTranslation = surahData.EnglishTranslation,
                    NumberOfAyahs = surahData.NumberOfAyahs,
                    RevelationType = surahData.RevelationType
                };
                _context.Surahs.Add(surah);

                if (surahData.Verses != null)
                {
                    foreach (var verseData in surahData.Verses)
                    {
                        var verse = new QuranVerse
                        {
                            SurahNumber = surahData.Number,
                            AyahNumber = verseData.AyahNumber,
                            ArabicText = verseData.ArabicText,
                            EnglishTranslation = verseData.EnglishTranslation,
                            Transliteration = verseData.Transliteration,
                            AudioUrl = verseData.AudioUrl,
                            JuzNumber = verseData.JuzNumber,
                            PageNumber = verseData.PageNumber
                        };
                        _context.QuranVerses.Add(verse);
                    }
                }

                result.ItemsImported++;
            }

            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = $"Successfully imported {result.ItemsImported} Surahs";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error importing Quran data: {ex.Message}";
        }

        return result;
    }
}

public class SurahImportData
{
    public int Number { get; set; }
    public string ArabicName { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string? EnglishTranslation { get; set; }
    public int NumberOfAyahs { get; set; }
    public string RevelationType { get; set; } = string.Empty;
    public List<VerseImportData>? Verses { get; set; }
}

public class VerseImportData
{
    public int AyahNumber { get; set; }
    public string ArabicText { get; set; } = string.Empty;
    public string? EnglishTranslation { get; set; }
    public string? Transliteration { get; set; }
    public string? AudioUrl { get; set; }
    public int JuzNumber { get; set; }
    public int PageNumber { get; set; }
}

public class ImportResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int ItemsImported { get; set; }
}
