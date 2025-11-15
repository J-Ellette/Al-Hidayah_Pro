namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a user's attempt at a quiz
/// </summary>
public class QuizAttempt
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Quiz ID
    /// </summary>
    public int QuizId { get; set; }
    
    /// <summary>
    /// Start time
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// End time
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Score achieved (0-100)
    /// </summary>
    public decimal Score { get; set; } = 0;
    
    /// <summary>
    /// Total questions
    /// </summary>
    public int TotalQuestions { get; set; }
    
    /// <summary>
    /// Correct answers count
    /// </summary>
    public int CorrectAnswers { get; set; } = 0;
    
    /// <summary>
    /// Did the user pass?
    /// </summary>
    public bool Passed { get; set; } = false;
    
    /// <summary>
    /// Time taken in seconds
    /// </summary>
    public int TimeTakenSeconds { get; set; } = 0;
    
    /// <summary>
    /// User's answers (JSON)
    /// </summary>
    public string? Answers { get; set; }
    
    // Navigation properties
    public User? User { get; set; }
    public Quiz? Quiz { get; set; }
}
