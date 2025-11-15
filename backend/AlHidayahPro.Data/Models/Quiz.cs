namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a quiz
/// </summary>
public class Quiz
{
    public int Id { get; set; }
    
    /// <summary>
    /// Title of the quiz
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Category (quran, hadith, islamic_history, arabic, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Difficulty level
    /// </summary>
    public string DifficultyLevel { get; set; } = "beginner";
    
    /// <summary>
    /// Time limit in minutes (0 for no limit)
    /// </summary>
    public int TimeLimitMinutes { get; set; } = 0;
    
    /// <summary>
    /// Passing score percentage
    /// </summary>
    public int PassingScore { get; set; } = 70;
    
    /// <summary>
    /// Is this quiz active/published?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Created date
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
    public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
}
