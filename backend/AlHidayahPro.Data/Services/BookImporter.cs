using System.Text.Json;
using AlHidayahPro.Data.Models;
using AlHidayahPro.Data.Data;

namespace AlHidayahPro.Data.Services;

/// <summary>
/// Service for importing Islamic books
/// </summary>
public class BookImporter
{
    private readonly IslamicDbContext _context;
    private readonly ModuleService _moduleService;

    public BookImporter(IslamicDbContext context, ModuleService moduleService)
    {
        _context = context;
        _moduleService = moduleService;
    }

    /// <summary>
    /// Import a book from JSON file
    /// </summary>
    public async Task<ImportResult> ImportBookAsync(string jsonFilePath, string moduleId)
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
            var bookData = JsonSerializer.Deserialize<BookImportData>(jsonContent);

            if (bookData == null)
            {
                result.Success = false;
                result.Message = "Invalid book data in JSON file";
                return result;
            }

            // Get or create module
            var module = await _moduleService.GetModuleAsync(moduleId);
            if (module == null)
            {
                var moduleData = new ModuleRegistrationData
                {
                    ModuleId = moduleId,
                    Name = bookData.Title,
                    Type = ModuleType.Book,
                    Language = bookData.Language,
                    Author = bookData.Author,
                    Description = bookData.Description,
                    Version = "1.0.0",
                    FileSize = new FileInfo(jsonFilePath).Length,
                    License = bookData.License
                };
                module = await _moduleService.RegisterModuleAsync(moduleData);
            }

            // Clear existing book for this module
            var existingBooks = _context.Books
                .Where(b => b.ModuleId == module.Id)
                .ToList();
            _context.Books.RemoveRange(existingBooks);
            await _context.SaveChangesAsync();

            // Create book
            var book = new Book
            {
                ModuleId = module.Id,
                Title = bookData.Title,
                Author = bookData.Author,
                Category = bookData.Category,
                Language = bookData.Language,
                Description = bookData.Description,
                ISBN = bookData.ISBN,
                Publisher = bookData.Publisher,
                PublicationYear = bookData.PublicationYear,
                PageCount = bookData.PageCount,
                CoverImageUrl = bookData.CoverImageUrl
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Import chapters
            if (bookData.Chapters != null && bookData.Chapters.Any())
            {
                foreach (var chapterData in bookData.Chapters)
                {
                    var chapter = new BookChapter
                    {
                        BookId = book.Id,
                        ChapterNumber = chapterData.ChapterNumber,
                        Title = chapterData.Title,
                        Content = chapterData.Content,
                        Summary = chapterData.Summary,
                        PageNumber = chapterData.PageNumber
                    };
                    _context.BookChapters.Add(chapter);
                    result.ItemsImported++;
                }
                await _context.SaveChangesAsync();
            }

            // Mark module as installed
            await _moduleService.MarkModuleInstalledAsync(moduleId);

            result.Success = true;
            result.Message = $"Successfully imported book with {result.ItemsImported} chapters";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Error importing book: {ex.Message}";
        }

        return result;
    }
}

public class BookImportData
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public string? Publisher { get; set; }
    public int? PublicationYear { get; set; }
    public int? PageCount { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? License { get; set; }
    public List<ChapterImportData>? Chapters { get; set; }
}

public class ChapterImportData
{
    public int ChapterNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public int? PageNumber { get; set; }
}
