using Microsoft.AspNetCore.Mvc;
using AlHidayahPro.Data;
using AlHidayahPro.Data.Data;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// Admin controller for data management and import operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IslamicDbContext _context;
    private readonly ILogger<AdminController> _logger;
    private readonly IWebHostEnvironment _environment;

    public AdminController(
        IslamicDbContext context,
        ILogger<AdminController> logger,
        IWebHostEnvironment environment)
    {
        _context = context;
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    /// Import full Quran data from JSON file
    /// </summary>
    [HttpPost("import/quran")]
    public async Task<ActionResult<ImportResult>> ImportQuranData([FromBody] ImportRequest request)
    {
        try
        {
            var filePath = Path.Combine(_environment.ContentRootPath, request.FilePath);
            var importer = new QuranDataImporter(_context);
            var result = await importer.ImportQuranDataAsync(filePath);

            if (result.Success)
            {
                _logger.LogInformation("Quran data imported successfully: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Failed to import Quran data: {Message}", result.Message);
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Quran data import");
            return StatusCode(500, new ImportResult
            {
                Success = false,
                Message = "Internal server error during import"
            });
        }
    }

    /// <summary>
    /// Import Hadith collection from JSON file
    /// </summary>
    [HttpPost("import/hadith")]
    public async Task<ActionResult<ImportResult>> ImportHadithData([FromBody] HadithImportRequest request)
    {
        try
        {
            var filePath = Path.Combine(_environment.ContentRootPath, request.FilePath);
            var importer = new HadithDataImporter(_context);
            var result = await importer.ImportHadithDataAsync(filePath, request.Collection);

            if (result.Success)
            {
                _logger.LogInformation("Hadith data imported successfully: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Failed to import Hadith data: {Message}", result.Message);
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during Hadith data import");
            return StatusCode(500, new ImportResult
            {
                Success = false,
                Message = "Internal server error during import"
            });
        }
    }

    /// <summary>
    /// Get database statistics
    /// </summary>
    [HttpGet("stats")]
    public ActionResult<DatabaseStats> GetDatabaseStats()
    {
        try
        {
            var stats = new DatabaseStats
            {
                TotalSurahs = _context.Surahs.Count(),
                TotalVerses = _context.QuranVerses.Count(),
                TotalHadiths = _context.Hadiths.Count(),
                TotalRecitations = _context.AudioRecitations.Count(),
                Collections = _context.Hadiths.Select(h => h.Collection).Distinct().ToList()
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching database stats");
            return StatusCode(500, "Internal server error");
        }
    }
}

public class ImportRequest
{
    public string FilePath { get; set; } = string.Empty;
}

public class HadithImportRequest : ImportRequest
{
    public string Collection { get; set; } = string.Empty;
}

public class DatabaseStats
{
    public int TotalSurahs { get; set; }
    public int TotalVerses { get; set; }
    public int TotalHadiths { get; set; }
    public int TotalRecitations { get; set; }
    public List<string> Collections { get; set; } = new();
}
