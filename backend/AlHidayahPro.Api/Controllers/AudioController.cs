using Microsoft.AspNetCore.Mvc;
using AlHidayahPro.Api.Services;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// API controller for Audio Recitation operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AudioController : ControllerBase
{
    private readonly IAudioService _audioService;
    private readonly ILogger<AudioController> _logger;

    public AudioController(IAudioService audioService, ILogger<AudioController> logger)
    {
        _audioService = audioService;
        _logger = logger;
    }

    /// <summary>
    /// Get list of available reciters
    /// </summary>
    [HttpGet("reciters")]
    public async Task<ActionResult<List<string>>> GetReciters()
    {
        try
        {
            var reciters = await _audioService.GetRecitersAsync();
            return Ok(reciters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching reciters");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get recitation for a specific verse or surah
    /// </summary>
    [HttpGet("recitation/{reciterName}/surah/{surahNumber}")]
    public async Task<ActionResult<AudioRecitationDto>> GetRecitation(
        string reciterName, 
        int surahNumber, 
        [FromQuery] int? ayahNumber = null)
    {
        try
        {
            if (surahNumber < 1 || surahNumber > 114)
            {
                return BadRequest("Surah number must be between 1 and 114");
            }

            var recitation = await _audioService.GetRecitationAsync(reciterName, surahNumber, ayahNumber);
            if (recitation == null)
            {
                return NotFound($"Recitation not found for {reciterName} - Surah {surahNumber}");
            }

            return Ok(recitation);
        }
        catch (Exception ex)
        {
            var sanitizedReciter = reciterName.Replace("\r", "").Replace("\n", "");
            _logger.LogError(ex, "Error fetching recitation for {ReciterName} - Surah {SurahNumber}", sanitizedReciter, surahNumber);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get all recitations for a surah by a reciter
    /// </summary>
    [HttpGet("recitation/{reciterName}/surah/{surahNumber}/all")]
    public async Task<ActionResult<List<AudioRecitationDto>>> GetRecitationsBySurah(string reciterName, int surahNumber)
    {
        try
        {
            if (surahNumber < 1 || surahNumber > 114)
            {
                return BadRequest("Surah number must be between 1 and 114");
            }

            var recitations = await _audioService.GetRecitationsBySurahAsync(reciterName, surahNumber);
            return Ok(recitations);
        }
        catch (Exception ex)
        {
            var sanitizedReciter = reciterName.Replace("\r", "").Replace("\n", "");
            _logger.LogError(ex, "Error fetching recitations for {ReciterName} - Surah {SurahNumber}", sanitizedReciter, surahNumber);
            return StatusCode(500, "Internal server error");
        }
    }
}
