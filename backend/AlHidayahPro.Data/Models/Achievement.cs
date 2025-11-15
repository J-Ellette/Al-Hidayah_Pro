namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents an achievement/badge that can be earned
/// </summary>
public class Achievement
{
    public int Id { get; set; }
    
    /// <summary>
    /// Unique identifier for the achievement
    /// </summary>
    public string AchievementId { get; set; } = string.Empty;
    
    /// <summary>
    /// Title of the achievement
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Icon/badge identifier
    /// </summary>
    public string IconName { get; set; } = string.Empty;
    
    /// <summary>
    /// Category (reading, memorization, study, streak, etc.)
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Points awarded for earning this achievement
    /// </summary>
    public int Points { get; set; }
    
    /// <summary>
    /// Tier (bronze, silver, gold, platinum)
    /// </summary>
    public string Tier { get; set; } = "bronze";
    
    /// <summary>
    /// Is this achievement hidden until earned?
    /// </summary>
    public bool IsHidden { get; set; } = false;
    
    /// <summary>
    /// Criteria for earning (JSON string)
    /// </summary>
    public string? Criteria { get; set; }
    
    // Navigation property
    public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
