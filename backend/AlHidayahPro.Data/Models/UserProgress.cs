namespace AlHidayahPro.Data.Models;

/// <summary>
/// Tracks user's progress across the platform
/// </summary>
public class UserProgress
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Total points/score earned
    /// </summary>
    public int TotalPoints { get; set; } = 0;
    
    /// <summary>
    /// Current level
    /// </summary>
    public int Level { get; set; } = 1;
    
    /// <summary>
    /// Experience points for current level
    /// </summary>
    public int ExperiencePoints { get; set; } = 0;
    
    /// <summary>
    /// Quran verses memorized count
    /// </summary>
    public int VersesMemorized { get; set; } = 0;
    
    /// <summary>
    /// Quran verses read count
    /// </summary>
    public int VersesRead { get; set; } = 0;
    
    /// <summary>
    /// Hadiths studied count
    /// </summary>
    public int HadithsStudied { get; set; } = 0;
    
    /// <summary>
    /// Lessons completed count
    /// </summary>
    public int LessonsCompleted { get; set; } = 0;
    
    /// <summary>
    /// Quizzes completed count
    /// </summary>
    public int QuizzesCompleted { get; set; } = 0;
    
    /// <summary>
    /// Quiz average score percentage
    /// </summary>
    public decimal AverageQuizScore { get; set; } = 0;
    
    /// <summary>
    /// Current streak (consecutive days)
    /// </summary>
    public int CurrentStreak { get; set; } = 0;
    
    /// <summary>
    /// Longest streak ever
    /// </summary>
    public int LongestStreak { get; set; } = 0;
    
    /// <summary>
    /// Last activity date
    /// </summary>
    public DateTime? LastActivityDate { get; set; }
    
    /// <summary>
    /// Total study time in minutes
    /// </summary>
    public int TotalStudyMinutes { get; set; } = 0;
    
    // Navigation property
    public User? User { get; set; }
}
