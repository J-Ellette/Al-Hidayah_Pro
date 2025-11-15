using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service for spaced repetition (flashcard) functionality using SM-2 algorithm
/// </summary>
public class SpacedRepetitionService : ISpacedRepetitionService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILogger<SpacedRepetitionService> _logger;

    public SpacedRepetitionService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILogger<SpacedRepetitionService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    public async Task<IEnumerable<FlashCard>> GetFlashCardsAsync(string? category = null)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var query = context.FlashCards.Where(fc => fc.IsActive);

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(fc => fc.Category == category);
        }

        return await query.ToListAsync();
    }

    public async Task<FlashCard?> GetFlashCardAsync(int cardId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.FlashCards.FindAsync(cardId);
    }

    public async Task<IEnumerable<FlashCard>> GetDueFlashCardsAsync(int userId, int limit = 20)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var today = DateTime.UtcNow.Date;
        
        // Get cards that are due for review
        var dueCards = await context.UserFlashCardProgresses
            .Where(p => p.UserId == userId && 
                       p.NextReviewDate.Date <= today &&
                       !p.IsMastered)
            .Include(p => p.FlashCard)
            .OrderBy(p => p.NextReviewDate)
            .Take(limit)
            .Select(p => p.FlashCard!)
            .ToListAsync();

        // If not enough due cards, add new cards
        if (dueCards.Count < limit)
        {
            var learnedCardIds = await context.UserFlashCardProgresses
                .Where(p => p.UserId == userId)
                .Select(p => p.FlashCardId)
                .ToListAsync();

            var newCards = await context.FlashCards
                .Where(fc => fc.IsActive && !learnedCardIds.Contains(fc.Id))
                .Take(limit - dueCards.Count)
                .ToListAsync();

            dueCards.AddRange(newCards);
        }

        return dueCards;
    }

    public async Task<UserFlashCardProgress> ReviewFlashCardAsync(int userId, int cardId, int quality)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        // Validate quality (0-5 scale)
        if (quality < 0 || quality > 5)
        {
            throw new ArgumentException("Quality must be between 0 and 5", nameof(quality));
        }

        var progress = await context.UserFlashCardProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.FlashCardId == cardId);

        if (progress == null)
        {
            // First review
            progress = new UserFlashCardProgress
            {
                UserId = userId,
                FlashCardId = cardId,
                EaseFactor = 2.5m,
                IntervalDays = 0,
                Repetitions = 0
            };
            context.UserFlashCardProgresses.Add(progress);
        }

        // Apply SM-2 algorithm
        progress = ApplySM2Algorithm(progress, quality);
        progress.LastReviewDate = DateTime.UtcNow;
        progress.TotalReviews++;

        // Update success rate
        var successfulReviews = quality >= 3 ? 1 : 0;
        progress.SuccessRate = ((progress.SuccessRate * (progress.TotalReviews - 1)) + (successfulReviews * 100)) 
                              / progress.TotalReviews;

        // Mark as mastered if consistently good
        if (progress.SuccessRate >= 90 && progress.TotalReviews >= 10 && progress.IntervalDays >= 30)
        {
            progress.IsMastered = true;
        }

        await context.SaveChangesAsync();

        _logger.LogInformation("FlashCard {CardId} reviewed by user {UserId} with quality {Quality}", 
            cardId, userId, quality);

        return progress;
    }

    public async Task<UserFlashCardProgress?> GetCardProgressAsync(int userId, int cardId)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        return await context.UserFlashCardProgresses
            .Include(p => p.FlashCard)
            .FirstOrDefaultAsync(p => p.UserId == userId && p.FlashCardId == cardId);
    }

    /// <summary>
    /// Applies the SM-2 spaced repetition algorithm
    /// Quality scale: 0-5 where 3+ is considered successful
    /// </summary>
    private UserFlashCardProgress ApplySM2Algorithm(UserFlashCardProgress progress, int quality)
    {
        if (quality >= 3)
        {
            // Successful recall
            if (progress.Repetitions == 0)
            {
                progress.IntervalDays = 1;
            }
            else if (progress.Repetitions == 1)
            {
                progress.IntervalDays = 6;
            }
            else
            {
                progress.IntervalDays = (int)Math.Round(progress.IntervalDays * (double)progress.EaseFactor);
            }

            progress.Repetitions++;
        }
        else
        {
            // Failed recall - reset repetitions and interval
            progress.Repetitions = 0;
            progress.IntervalDays = 1;
        }

        // Update ease factor
        progress.EaseFactor = progress.EaseFactor + (0.1m - (5 - quality) * (0.08m + (5 - quality) * 0.02m));

        // Ensure ease factor doesn't go below 1.3
        if (progress.EaseFactor < 1.3m)
        {
            progress.EaseFactor = 1.3m;
        }

        // Set next review date
        progress.NextReviewDate = DateTime.UtcNow.AddDays(progress.IntervalDays);

        return progress;
    }
}
