using System.Text.Json;
using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Data.Data;

/// <summary>
/// Service for importing Hadith collections from JSON files
/// </summary>
public class HadithDataImporter
{
    private readonly IslamicDbContext _context;

    public HadithDataImporter(IslamicDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Import Hadith collection from a JSON file
    /// Expected format: Array of Hadith objects
    /// </summary>
    public async Task<ImportResult> ImportHadithDataAsync(string jsonFilePath, string collection)
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
            var hadithData = JsonSerializer.Deserialize<List<HadithImportData>>(jsonContent);

            if (hadithData == null || !hadithData.Any())
            {
                result.Success = false;
                result.Message = "No data found in JSON file";
                return result;
            }

            // Remove existing hadiths from this collection if reimporting
            var existingHadiths = _context.Hadiths
                .Where(h => h.Collection.ToLower() == collection.ToLower())
                .ToList();
            _context.Hadiths.RemoveRange(existingHadiths);
            await _context.SaveChangesAsync();

            foreach (var hadithImport in hadithData)
            {
                var hadith = new Hadith
                {
                    Collection = collection,
                    Book = hadithImport.Book,
                    BookNumber = hadithImport.BookNumber,
                    HadithNumber = hadithImport.HadithNumber,
                    ArabicText = hadithImport.ArabicText,
                    EnglishText = hadithImport.EnglishText,
                    Grade = hadithImport.Grade,
                    Narrator = hadithImport.Narrator,
                    Chapter = hadithImport.Chapter
                };
                _context.Hadiths.Add(hadith);
                result.ItemsImported++;
            }

            await _context.SaveChangesAsync();
            result.Success = true;
            result.Message = $"Successfully imported {result.ItemsImported} Hadiths from {collection}";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error importing Hadith data: {ex.Message}";
        }

        return result;
    }
}

public class HadithImportData
{
    public string Book { get; set; } = string.Empty;
    public int BookNumber { get; set; }
    public string HadithNumber { get; set; } = string.Empty;
    public string? ArabicText { get; set; }
    public string EnglishText { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string? Narrator { get; set; }
    public string? Chapter { get; set; }
}
