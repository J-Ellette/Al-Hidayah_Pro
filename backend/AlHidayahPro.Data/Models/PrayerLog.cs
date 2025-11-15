namespace AlHidayahPro.Data.Models;

/// <summary>
/// Represents a log entry for a completed prayer
/// </summary>
public class PrayerLog
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Prayer name (Fajr, Dhuhr, Asr, Maghrib, Isha)
    /// </summary>
    public string PrayerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Date and time when prayer was logged
    /// </summary>
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date of the prayer (for daily tracking)
    /// </summary>
    public DateTime PrayerDate { get; set; }
    
    /// <summary>
    /// Was this prayer performed on time?
    /// </summary>
    public bool OnTime { get; set; } = true;
    
    /// <summary>
    /// Was this prayer performed in congregation (jamaah)?
    /// </summary>
    public bool InCongregation { get; set; } = false;
    
    /// <summary>
    /// Location where prayer was performed (optional)
    /// </summary>
    public string? Location { get; set; }
    
    /// <summary>
    /// Optional notes
    /// </summary>
    public string? Notes { get; set; }
    
    // Navigation property
    public User? User { get; set; }
}
