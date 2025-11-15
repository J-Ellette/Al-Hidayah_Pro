namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents an achievement earned by a user
/// </summary>
public class UserAchievement
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Achievement ID
    /// </summary>
    public int AchievementId { get; set; }
    
    /// <summary>
    /// Date earned
    /// </summary>
    public DateTime EarnedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Progress towards next tier (if applicable)
    /// </summary>
    public int Progress { get; set; } = 0;
    
    // Navigation properties
    public User? User { get; set; }
    public Achievement? Achievement { get; set; }
}
