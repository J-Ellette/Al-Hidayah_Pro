namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a user's participation in a challenge
/// </summary>
public class UserChallenge
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Challenge ID
    /// </summary>
    public int ChallengeId { get; set; }
    
    /// <summary>
    /// Current progress value
    /// </summary>
    public int CurrentValue { get; set; } = 0;
    
    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;
    
    /// <summary>
    /// Join date
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Completion date
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Is this challenge completed?
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Last activity date
    /// </summary>
    public DateTime? LastActivityAt { get; set; }
    
    // Navigation properties
    public User? User { get; set; }
    public Challenge? Challenge { get; set; }
}
