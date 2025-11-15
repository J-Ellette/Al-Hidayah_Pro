namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a flashcard for spaced repetition learning
/// </summary>
public class FlashCard
{
    public int Id { get; set; }
    
    /// <summary>
    /// Front of the card (question/prompt)
    /// </summary>
    public string Front { get; set; } = string.Empty;
    
    /// <summary>
    /// Back of the card (answer)
    /// </summary>
    public string Back { get; set; } = string.Empty;
    
    /// <summary>
    /// Category (verse_memorization, arabic_vocab, hadith, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Difficulty level
    /// </summary>
    public string DifficultyLevel { get; set; } = "beginner";
    
    /// <summary>
    /// Reference (e.g., Surah 2:255, or vocabulary list name)
    /// </summary>
    public string? Reference { get; set; }
    
    /// <summary>
    /// Additional notes or context
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Is this card active?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Created date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public ICollection<UserFlashCardProgress> UserProgress { get; set; } = new List<UserFlashCardProgress>();
}
