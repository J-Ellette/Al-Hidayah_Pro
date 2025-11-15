using AlHidayahPro.Data.Models;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service interface for spaced repetition (flashcard) functionality
/// </summary>
public interface ISpacedRepetitionService
{
    Task<IEnumerable<FlashCard>> GetFlashCardsAsync(string? category = null);
    Task<FlashCard?> GetFlashCardAsync(int cardId);
    Task<IEnumerable<FlashCard>> GetDueFlashCardsAsync(int userId, int limit = 20);
    Task<UserFlashCardProgress> ReviewFlashCardAsync(int userId, int cardId, int quality);
    Task<UserFlashCardProgress?> GetCardProgressAsync(int userId, int cardId);
}
