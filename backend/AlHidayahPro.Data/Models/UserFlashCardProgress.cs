namespace AlHidayahPro.Data.Models;

/// <summary>
/// Tracks user's progress on a flashcard (spaced repetition data)
/// </summary>
public class UserFlashCardProgress
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// FlashCard ID
    /// </summary>
    public int FlashCardId { get; set; }
    
    /// <summary>
    /// Ease factor (SM-2 algorithm) - typically starts at 2.5
    /// </summary>
    public decimal EaseFactor { get; set; } = 2.5m;
    
    /// <summary>
    /// Interval in days until next review
    /// </summary>
    public int IntervalDays { get; set; } = 0;
    
    /// <summary>
    /// Number of repetitions
    /// </summary>
    public int Repetitions { get; set; } = 0;
    
    /// <summary>
    /// Next review date
    /// </summary>
    public DateTime NextReviewDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Last review date
    /// </summary>
    public DateTime? LastReviewDate { get; set; }
    
    /// <summary>
    /// Total times reviewed
    /// </summary>
    public int TotalReviews { get; set; } = 0;
    
    /// <summary>
    /// Success rate (0-100)
    /// </summary>
    public decimal SuccessRate { get; set; } = 0;
    
    /// <summary>
    /// Is this card marked as learned/mastered?
    /// </summary>
    public bool IsMastered { get; set; } = false;
    
    // Navigation properties
    public User? User { get; set; }
    public FlashCard? FlashCard { get; set; }
}
