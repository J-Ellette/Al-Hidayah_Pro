using Microsoft.AspNetCore.Mvc;
using AlHidayahPro.Api.Services;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// API controller for Quran-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class QuranController : ControllerBase
{
    private readonly IQuranService _quranService;
    private readonly ILogger<QuranController> _logger;

    public QuranController(IQuranService quranService, ILogger<QuranController> logger)
    {
        _quranService = quranService;
        _logger = logger;
    }

    /// <summary>
    /// Get all Surahs (chapters) list
    /// </summary>
    [HttpGet("surahs")]
    public async Task<ActionResult<List<SurahDto>>> GetAllSurahs()
    {
        try
        {
            var surahs = await _quranService.GetAllSurahsAsync();
            return Ok(surahs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching all surahs");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get a specific Surah by number
    /// </summary>
    [HttpGet("surah/{surahNumber}")]
    public async Task<ActionResult<SurahDto>> GetSurah(int surahNumber)
    {
        try
        {
            if (surahNumber < 1 || surahNumber > 114)
            {
                return BadRequest("Surah number must be between 1 and 114");
            }

            var surah = await _quranService.GetSurahAsync(surahNumber);
            if (surah == null)
            {
                return NotFound($"Surah {surahNumber} not found");
            }

            return Ok(surah);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching surah {SurahNumber}", surahNumber);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get a specific verse
    /// </summary>
    [HttpGet("surah/{surahNumber}/ayah/{ayahNumber}")]
    public async Task<ActionResult<QuranVerseDto>> GetVerse(int surahNumber, int ayahNumber)
    {
        try
        {
            if (surahNumber < 1 || surahNumber > 114)
            {
                return BadRequest("Surah number must be between 1 and 114");
            }

            var verse = await _quranService.GetVerseAsync(surahNumber, ayahNumber);
            if (verse == null)
            {
                return NotFound($"Verse {surahNumber}:{ayahNumber} not found");
            }

            return Ok(verse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching verse {SurahNumber}:{AyahNumber}", surahNumber, ayahNumber);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get all verses for a Surah
    /// </summary>
    [HttpGet("surah/{surahNumber}/ayahs")]
    public async Task<ActionResult<List<QuranVerseDto>>> GetVersesBySurah(int surahNumber)
    {
        try
        {
            if (surahNumber < 1 || surahNumber > 114)
            {
                return BadRequest("Surah number must be between 1 and 114");
            }

            var verses = await _quranService.GetVersesBySurahAsync(surahNumber);
            return Ok(verses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching verses for surah {SurahNumber}", surahNumber);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Search verses by text
    /// </summary>
    [HttpPost("search")]
    public async Task<ActionResult<QuranSearchResults>> SearchVerses([FromBody] QuranSearchRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Query))
            {
                return BadRequest("Search query is required");
            }

            var results = await _quranService.SearchVersesAsync(request);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching verses");
            return StatusCode(500, "Internal server error");
        }
    }
}
