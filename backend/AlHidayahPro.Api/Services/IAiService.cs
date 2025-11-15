namespace AlHidayahPro.Api.Services;

/// <summary>
/// Interface for AI service with fall-back support
/// </summary>
public interface IAiService
{
    /// <summary>
    /// Get a text completion from AI with automatic fall-back
    /// </summary>
    Task<AiResponse> GetCompletionAsync(string prompt, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Check if AI service is available
    /// </summary>
    Task<bool> IsAvailableAsync();
    
    /// <summary>
    /// Get the current provider name
    /// </summary>
    string GetProviderName();
}

/// <summary>
/// Response from AI service
/// </summary>
public class AiResponse
{
    public string Content { get; set; } = string.Empty;
    public bool UsedFallback { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public bool Success => string.IsNullOrEmpty(ErrorMessage);
}
