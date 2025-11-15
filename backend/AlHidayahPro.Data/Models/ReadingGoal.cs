namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a Quran reading goal
/// </summary>
public class ReadingGoal
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Title of the goal (e.g., "Complete Quran in 30 days")
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Type of goal (daily_pages, complete_quran, specific_surahs, etc.)
    /// </summary>
    public string GoalType { get; set; } = string.Empty;
    
    /// <summary>
    /// Target value (e.g., number of pages per day, number of surahs)
    /// </summary>
    public int TargetValue { get; set; }
    
    /// <summary>
    /// Current progress value
    /// </summary>
    public int CurrentValue { get; set; } = 0;
    
    /// <summary>
    /// Start date of the goal
    /// </summary>
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Target completion date
    /// </summary>
    public DateTime? TargetDate { get; set; }
    
    /// <summary>
    /// Actual completion date
    /// </summary>
    public DateTime? CompletedDate { get; set; }
    
    /// <summary>
    /// Is this goal active?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;
    
    // Navigation property
    public User? User { get; set; }
}
