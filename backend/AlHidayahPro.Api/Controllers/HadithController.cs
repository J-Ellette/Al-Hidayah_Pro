using Microsoft.AspNetCore.Mvc;
using AlHidayahPro.Api.Services;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// API controller for Hadith-related operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HadithController : ControllerBase
{
    private readonly IHadithService _hadithService;
    private readonly ILogger<HadithController> _logger;

    public HadithController(IHadithService hadithService, ILogger<HadithController> logger)
    {
        _hadithService = hadithService;
        _logger = logger;
    }

    /// <summary>
    /// Get list of available Hadith collections
    /// </summary>
    [HttpGet("collections")]
    public async Task<ActionResult<List<string>>> GetCollections()
    {
        try
        {
            var collections = await _hadithService.GetCollectionsAsync();
            return Ok(collections);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching collections");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get Hadiths by collection
    /// </summary>
    [HttpGet("collection/{collection}")]
    public async Task<ActionResult<List<HadithDto>>> GetHadithsByCollection(string collection, [FromQuery] int? bookNumber = null)
    {
        try
        {
            var hadiths = await _hadithService.GetHadithsByCollectionAsync(collection, bookNumber);
            return Ok(hadiths);
        }
        catch (Exception ex)
        {
            var sanitizedCollection = collection.Replace("\r", "").Replace("\n", "");
            _logger.LogError(ex, "Error fetching hadiths from collection {Collection}", sanitizedCollection);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get a specific Hadith by number
    /// </summary>
    [HttpGet("collection/{collection}/hadith/{hadithNumber}")]
    public async Task<ActionResult<HadithDto>> GetHadithByNumber(string collection, string hadithNumber)
    {
        try
        {
            var hadith = await _hadithService.GetHadithByNumberAsync(collection, hadithNumber);
            if (hadith == null)
            {
                return NotFound($"Hadith {hadithNumber} not found in {collection}");
            }

            return Ok(hadith);
        }
        catch (Exception ex)
        {
            var sanitizedNumber = hadithNumber.Replace("\r", "").Replace("\n", "");
            var sanitizedCollection = collection.Replace("\r", "").Replace("\n", "");
            _logger.LogError(ex, "Error fetching hadith {HadithNumber} from {Collection}", sanitizedNumber, sanitizedCollection);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Search Hadiths
    /// </summary>
    [HttpPost("search")]
    public async Task<ActionResult<HadithSearchResults>> SearchHadith([FromBody] HadithSearchRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Query))
            {
                return BadRequest("Search query is required");
            }

            var results = await _hadithService.SearchHadithAsync(request);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching hadiths");
            return StatusCode(500, "Internal server error");
        }
    }
}
