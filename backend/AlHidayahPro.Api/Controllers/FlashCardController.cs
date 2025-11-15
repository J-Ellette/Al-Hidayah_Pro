using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlashCardController : ControllerBase
{
    private readonly ISpacedRepetitionService _spacedRepetitionService;
    private readonly ILogger<FlashCardController> _logger;

    public FlashCardController(
        ISpacedRepetitionService spacedRepetitionService,
        ILogger<FlashCardController> logger)
    {
        _spacedRepetitionService = spacedRepetitionService;
        _logger = logger;
    }

    /// <summary>
    /// Get all flashcards (optionally filtered by category)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FlashCard>>> GetFlashCards([FromQuery] string? category = null)
    {
        var cards = await _spacedRepetitionService.GetFlashCardsAsync(category);
        return Ok(cards);
    }

    /// <summary>
    /// Get a specific flashcard
    /// </summary>
    [HttpGet("{cardId}")]
    public async Task<ActionResult<FlashCard>> GetFlashCard(int cardId)
    {
        var card = await _spacedRepetitionService.GetFlashCardAsync(cardId);
        if (card == null) return NotFound();
        return Ok(card);
    }

    /// <summary>
    /// Get flashcards due for review
    /// </summary>
    [HttpGet("due/{userId}")]
    public async Task<ActionResult<IEnumerable<FlashCard>>> GetDueCards(int userId, [FromQuery] int limit = 20)
    {
        var cards = await _spacedRepetitionService.GetDueFlashCardsAsync(userId, limit);
        return Ok(cards);
    }

    /// <summary>
    /// Review a flashcard (applies SM-2 algorithm)
    /// </summary>
    [HttpPost("{cardId}/review")]
    public async Task<ActionResult<UserFlashCardProgress>> ReviewCard(int cardId, [FromBody] ReviewCardRequest request)
    {
        try
        {
            var progress = await _spacedRepetitionService.ReviewFlashCardAsync(
                request.UserId, cardId, request.Quality);
            return Ok(progress);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get user's progress on a flashcard
    /// </summary>
    [HttpGet("{cardId}/progress/{userId}")]
    public async Task<ActionResult<UserFlashCardProgress>> GetCardProgress(int cardId, int userId)
    {
        var progress = await _spacedRepetitionService.GetCardProgressAsync(userId, cardId);
        if (progress == null) return NotFound();
        return Ok(progress);
    }
}

public class ReviewCardRequest
{
    public int UserId { get; set; }
    
    /// <summary>
    /// Quality of recall (0-5): 0=complete blackout, 5=perfect recall
    /// </summary>
    public int Quality { get; set; }
}
