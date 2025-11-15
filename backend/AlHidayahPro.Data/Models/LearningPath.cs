namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a learning path or study plan
/// </summary>
public class LearningPath
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID this learning path belongs to
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Title of the learning path (e.g., "Complete Quran Reading in 30 Days")
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of the learning path
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Type of learning path: quran_reading, arabic_basics, hadith_study, etc.
    /// </summary>
    public string PathType { get; set; } = string.Empty;
    
    /// <summary>
    /// Target completion date
    /// </summary>
    public DateTime? TargetDate { get; set; }
    
    /// <summary>
    /// Start date
    /// </summary>
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Completion date (null if not completed)
    /// </summary>
    public DateTime? CompletedDate { get; set; }
    
    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;
    
    /// <summary>
    /// Is this path active?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Difficulty level (beginner, intermediate, advanced)
    /// </summary>
    public string DifficultyLevel { get; set; } = "beginner";
    
    /// <summary>
    /// Daily time commitment in minutes
    /// </summary>
    public int DailyMinutes { get; set; }
    
    // Navigation properties
    public User? User { get; set; }
    public ICollection<LearningMilestone> Milestones { get; set; } = new List<LearningMilestone>();
}
