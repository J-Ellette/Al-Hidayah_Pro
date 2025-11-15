using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// AI service implementation with automatic fall-back support
/// </summary>
public class AiService : IAiService
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILogger<AiService> _logger;
    private AiSettings? _cachedSettings;
    private DateTime _cacheExpiry = DateTime.MinValue;

    public AiService(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILogger<AiService> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    public async Task<AiResponse> GetCompletionAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var settings = await GetSettingsAsync();
        
        // If AI is disabled, use fall-back immediately
        if (!settings.IsEnabled || settings.Provider == "none")
        {
            _logger.LogInformation("AI is disabled, using software fall-back");
            return await GetFallbackResponseAsync(prompt);
        }

        try
        {
            // Try to get AI response based on provider
            return settings.Provider.ToLowerInvariant() switch
            {
                "local" => await GetLocalAiResponseAsync(prompt, cancellationToken),
                "chatgpt" => await GetChatGptResponseAsync(prompt, settings.ApiKey, settings.ModelName, cancellationToken),
                "claude" => await GetClaudeResponseAsync(prompt, settings.ApiKey, settings.ModelName, cancellationToken),
                _ => await GetFallbackResponseAsync(prompt)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AI service failed, using fall-back");
            
            // Use fall-back if enabled
            if (settings.UseFallback)
            {
                return await GetFallbackResponseAsync(prompt);
            }
            
            return new AiResponse
            {
                ErrorMessage = "AI service unavailable and fall-back is disabled",
                Provider = settings.Provider
            };
        }
    }

    public async Task<bool> IsAvailableAsync()
    {
        var settings = await GetSettingsAsync();
        
        if (!settings.IsEnabled || settings.Provider == "none")
        {
            return false;
        }

        // For now, assume service is available if configured
        // In production, this would ping the actual service
        return !string.IsNullOrEmpty(settings.ApiKey) || settings.Provider == "local";
    }

    public string GetProviderName()
    {
        // This is synchronous, so we'll use cached settings
        return _cachedSettings?.Provider ?? "none";
    }

    private async Task<AiSettings> GetSettingsAsync()
    {
        // Use cache if valid
        if (_cachedSettings != null && DateTime.UtcNow < _cacheExpiry)
        {
            return _cachedSettings;
        }

        await using var context = await _dbFactory.CreateDbContextAsync();
        
        // Get global settings (UserId is null)
        var settings = await context.Set<AiSettings>()
            .FirstOrDefaultAsync(s => s.UserId == null);

        if (settings == null)
        {
            // Create default settings
            settings = new AiSettings
            {
                IsEnabled = false,
                Provider = "none",
                UseFallback = true
            };
        }

        // Cache for 5 minutes
        _cachedSettings = settings;
        _cacheExpiry = DateTime.UtcNow.AddMinutes(5);

        return settings;
    }

    private Task<AiResponse> GetLocalAiResponseAsync(string prompt, CancellationToken cancellationToken)
    {
        // Local AI implementation would go here
        // For now, return a placeholder response
        _logger.LogInformation("Local AI not yet implemented, using fall-back");
        return GetFallbackResponseAsync(prompt);
    }

    private async Task<AiResponse> GetChatGptResponseAsync(
        string prompt, 
        string? apiKey, 
        string? model, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("ChatGPT API key is not configured");
        }

        // ChatGPT API implementation would go here
        // For now, return a placeholder response
        _logger.LogInformation("ChatGPT integration not yet implemented, using fall-back");
        return await GetFallbackResponseAsync(prompt);
    }

    private async Task<AiResponse> GetClaudeResponseAsync(
        string prompt, 
        string? apiKey, 
        string? model, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Claude API key is not configured");
        }

        // Claude API implementation would go here
        // For now, return a placeholder response
        _logger.LogInformation("Claude integration not yet implemented, using fall-back");
        return await GetFallbackResponseAsync(prompt);
    }

    private Task<AiResponse> GetFallbackResponseAsync(string prompt)
    {
        // Software fall-back: provide a basic response without AI
        var response = new AiResponse
        {
            Content = "AI features are currently unavailable. This is a software fall-back response. " +
                     "Please configure AI settings or enable AI features in the settings page.",
            UsedFallback = true,
            Provider = "fallback"
        };

        return Task.FromResult(response);
    }
}
