using AlHidayahPro.Data;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiSettingsController : ControllerBase
{
    private readonly IDbContextFactory<IslamicDbContext> _dbFactory;
    private readonly ILogger<AiSettingsController> _logger;

    public AiSettingsController(
        IDbContextFactory<IslamicDbContext> dbFactory,
        ILogger<AiSettingsController> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    /// <summary>
    /// Get current AI settings (global settings)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<AiSettings>> GetSettings()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var settings = await context.Set<AiSettings>()
            .FirstOrDefaultAsync(s => s.UserId == null);

        if (settings == null)
        {
            // Return default settings
            return new AiSettings
            {
                IsEnabled = false,
                Provider = "none",
                UseFallback = true
            };
        }

        // Don't send API key to client (security)
        settings.ApiKey = string.IsNullOrEmpty(settings.ApiKey) ? null : "***CONFIGURED***";

        return settings;
    }

    /// <summary>
    /// Update AI settings
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AiSettings>> UpdateSettings([FromBody] AiSettingsDto dto)
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var settings = await context.Set<AiSettings>()
            .FirstOrDefaultAsync(s => s.UserId == null);

        if (settings == null)
        {
            // Create new settings
            settings = new AiSettings
            {
                UserId = null
            };
            context.Set<AiSettings>().Add(settings);
        }

        // Validate and sanitize provider input
        var validProviders = new[] { "none", "local", "chatgpt", "claude" };
        var sanitizedProvider = validProviders.Contains(dto.Provider?.ToLowerInvariant() ?? string.Empty) 
            ? dto.Provider!.ToLowerInvariant() 
            : "none";

        // Update settings
        settings.IsEnabled = dto.IsEnabled;
        settings.Provider = sanitizedProvider;
        settings.UseFallback = dto.UseFallback;
        settings.ModelName = dto.ModelName;
        settings.UpdatedAt = DateTime.UtcNow;

        // Only update API key if provided (not the placeholder)
        if (!string.IsNullOrEmpty(dto.ApiKey) && dto.ApiKey != "***CONFIGURED***")
        {
            // In production, this should be encrypted
            settings.ApiKey = dto.ApiKey;
        }

        await context.SaveChangesAsync();

        _logger.LogInformation("AI settings updated: Provider={Provider}, Enabled={Enabled}", 
            sanitizedProvider, settings.IsEnabled);

        // Don't send API key back
        settings.ApiKey = string.IsNullOrEmpty(settings.ApiKey) ? null : "***CONFIGURED***";

        return settings;
    }

    /// <summary>
    /// Test AI connection
    /// </summary>
    [HttpPost("test")]
    public async Task<ActionResult<AiTestResult>> TestConnection()
    {
        await using var context = await _dbFactory.CreateDbContextAsync();
        
        var settings = await context.Set<AiSettings>()
            .FirstOrDefaultAsync(s => s.UserId == null);

        if (settings == null || !settings.IsEnabled || settings.Provider == "none")
        {
            return new AiTestResult
            {
                Success = false,
                Message = "AI is not enabled or configured"
            };
        }

        // In production, this would actually test the connection
        // For now, just validate the configuration
        bool hasApiKey = !string.IsNullOrEmpty(settings.ApiKey);
        bool isLocal = settings.Provider == "local";

        if (!isLocal && !hasApiKey)
        {
            return new AiTestResult
            {
                Success = false,
                Message = $"API key required for {settings.Provider}"
            };
        }

        return new AiTestResult
        {
            Success = true,
            Message = $"Configuration valid for {settings.Provider}",
            Provider = settings.Provider
        };
    }
}

/// <summary>
/// DTO for updating AI settings
/// </summary>
public class AiSettingsDto
{
    public bool IsEnabled { get; set; }
    public string Provider { get; set; } = "none";
    public string? ApiKey { get; set; }
    public string? ModelName { get; set; }
    public bool UseFallback { get; set; } = true;
}

/// <summary>
/// Result of AI connection test
/// </summary>
public class AiTestResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Provider { get; set; }
}
