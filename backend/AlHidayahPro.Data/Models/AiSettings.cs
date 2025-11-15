namespace AlHidayahPro.Data.Models;

/// <summary>
/// Configuration for AI providers and settings
/// </summary>
public class AiSettings
{
    public int Id { get; set; }
    
    /// <summary>
    /// User ID this setting belongs to (null for global settings)
    /// </summary>
    public int? UserId { get; set; }
    
    /// <summary>
    /// Whether AI features are enabled
    /// </summary>
    public bool IsEnabled { get; set; } = false;
    
    /// <summary>
    /// Selected AI provider: "local", "chatgpt", "claude", or "none"
    /// </summary>
    public string Provider { get; set; } = "none";
    
    /// <summary>
    /// API key for the selected provider (encrypted in production)
    /// </summary>
    public string? ApiKey { get; set; }
    
    /// <summary>
    /// Model name for the provider (e.g., "gpt-4", "claude-3-opus")
    /// </summary>
    public string? ModelName { get; set; }
    
    /// <summary>
    /// Whether to use software fall-back when AI is unavailable
    /// </summary>
    public bool UseFallback { get; set; } = true;
    
    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public User? User { get; set; }
}
