namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a milestone or checkpoint in a learning path
/// </summary>
public class LearningMilestone
{
    public int Id { get; set; }
    
    /// <summary>
    /// Learning path this milestone belongs to
    /// </summary>
    public int LearningPathId { get; set; }
    
    /// <summary>
    /// Title of the milestone
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Order in the learning path
    /// </summary>
    public int OrderIndex { get; set; }
    
    /// <summary>
    /// Is this milestone completed?
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date completed
    /// </summary>
    public DateTime? CompletedDate { get; set; }
    
    /// <summary>
    /// Target date for this milestone
    /// </summary>
    public DateTime? TargetDate { get; set; }
    
    /// <summary>
    /// Points/score earned for completing this milestone
    /// </summary>
    public int Points { get; set; } = 0;
    
    // Navigation property
    public LearningPath? LearningPath { get; set; }
}
