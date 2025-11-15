using System.Text.Json;
using AlHidayahPro.Data.Models;
using AlHidayahPro.Data.Data;

namespace AlHidayahPro.Data.Services;

/// <summary>
/// Service for importing Quran translations
/// </summary>
public class TranslationImporter
{
    private readonly IslamicDbContext _context;
    private readonly ModuleService _moduleService;

    public TranslationImporter(IslamicDbContext context, ModuleService moduleService)
    {
        _context = context;
        _moduleService = moduleService;
    }

    /// <summary>
    /// Import a Quran translation from JSON file
    /// </summary>
    public async Task<ImportResult> ImportTranslationAsync(string jsonFilePath, string moduleId, string language, string translator)
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

            // Get or create module
            var module = await _moduleService.GetModuleAsync(moduleId);
            if (module == null)
            {
                // Register new module
                var moduleData = new ModuleRegistrationData
                {
                    ModuleId = moduleId,
                    Name = $"{translator} Translation ({language})",
                    Type = ModuleType.QuranTranslation,
                    Language = language,
                    Author = translator,
                    Description = $"Quran translation by {translator} in {language}",
                    Version = "1.0.0",
                    FileSize = new FileInfo(jsonFilePath).Length
                };
                module = await _moduleService.RegisterModuleAsync(moduleData);
            }

            var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
            var translationData = JsonSerializer.Deserialize<List<TranslationImportData>>(jsonContent);

            if (translationData == null || !translationData.Any())
            {
                result.Success = false;
                result.Message = "No translation data found in JSON file";
                return result;
            }

            // Clear existing translations for this module
            var existingTranslations = _context.QuranTranslations
                .Where(t => t.ModuleId == module.Id)
                .ToList();
            _context.QuranTranslations.RemoveRange(existingTranslations);
            await _context.SaveChangesAsync();

            // Import translations
            foreach (var item in translationData)
            {
                var translation = new QuranTranslation
                {
                    ModuleId = module.Id,
                    SurahNumber = item.SurahNumber,
                    AyahNumber = item.AyahNumber,
                    Translation = item.Translation,
                    Language = language,
                    Translator = translator,
                    Footnotes = item.Footnotes
                };
                _context.QuranTranslations.Add(translation);
                result.ItemsImported++;
            }

            await _context.SaveChangesAsync();

            // Mark module as installed
            await _moduleService.MarkModuleInstalledAsync(moduleId);

            result.Success = true;
            result.Message = $"Successfully imported {result.ItemsImported} translation verses";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error importing translation: {ex.Message}";
        }

        return result;
    }
}

public class TranslationImportData
{
    public int SurahNumber { get; set; }
    public int AyahNumber { get; set; }
    public string Translation { get; set; } = string.Empty;
    public string? Footnotes { get; set; }
}
