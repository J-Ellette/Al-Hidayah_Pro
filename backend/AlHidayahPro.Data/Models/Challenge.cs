namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a monthly reading or memorization challenge
/// </summary>
public class Challenge
{
    public int Id { get; set; }
    
    /// <summary>
    /// Challenge title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Challenge type (reading, memorization, prayer_streak, quiz)
    /// </summary>
    public string ChallengeType { get; set; } = string.Empty;
    
    /// <summary>
    /// Target value to achieve
    /// </summary>
    public int TargetValue { get; set; }
    
    /// <summary>
    /// Points awarded for completion
    /// </summary>
    public int RewardPoints { get; set; }
    
    /// <summary>
    /// Start date
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    /// End date
    /// </summary>
    public DateTime EndDate { get; set; }
    
    /// <summary>
    /// Is this challenge active?
    /// </summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>
    /// Difficulty level
    /// </summary>
    public string DifficultyLevel { get; set; } = "medium";
    
    /// <summary>
    /// Icon/badge for this challenge
    /// </summary>
    public string? IconName { get; set; }
    
    // Navigation property
    public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
}
