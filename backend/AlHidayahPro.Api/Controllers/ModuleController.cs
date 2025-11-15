using Microsoft.AspNetCore.Mvc;
using AlHidayahPro.Data.Models;
using AlHidayahPro.Data.Services;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// API controller for content module management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private readonly ModuleService _moduleService;
    private readonly TranslationImporter _translationImporter;
    private readonly BookImporter _bookImporter;
    private readonly ILogger<ModuleController> _logger;
    private readonly IWebHostEnvironment _environment;

    public ModuleController(
        ModuleService moduleService,
        TranslationImporter translationImporter,
        BookImporter bookImporter,
        ILogger<ModuleController> logger,
        IWebHostEnvironment environment)
    {
        _moduleService = moduleService;
        _translationImporter = translationImporter;
        _bookImporter = bookImporter;
        _logger = logger;
        _environment = environment;
    }

    /// <summary>
    /// Get all available modules
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ContentModule>>> GetAllModules()
    {
        try
        {
            var modules = await _moduleService.GetAllModulesAsync();
            return Ok(modules);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching modules");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get installed modules
    /// </summary>
    [HttpGet("installed")]
    public async Task<ActionResult<List<ContentModule>>> GetInstalledModules()
    {
        try
        {
            var modules = await _moduleService.GetInstalledModulesAsync();
            return Ok(modules);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching installed modules");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get modules by type
    /// </summary>
    [HttpGet("type/{type}")]
    public async Task<ActionResult<List<ContentModule>>> GetModulesByType(ModuleType type)
    {
        try
        {
            var modules = await _moduleService.GetModulesByTypeAsync(type);
            return Ok(modules);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching modules by type");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get a specific module
    /// </summary>
    [HttpGet("{moduleId}")]
    public async Task<ActionResult<ContentModule>> GetModule(string moduleId)
    {
        try
        {
            var module = await _moduleService.GetModuleAsync(moduleId);
            if (module == null)
            {
                return NotFound($"Module {moduleId} not found");
            }
            return Ok(module);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching module");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Register a new module
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ContentModule>> RegisterModule([FromBody] ModuleRegistrationData data)
    {
        try
        {
            var module = await _moduleService.RegisterModuleAsync(data);
            return CreatedAtAction(nameof(GetModule), new { moduleId = module.ModuleId }, module);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering module");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Import a Quran translation module
    /// </summary>
    [HttpPost("import/translation")]
    public async Task<ActionResult<Data.Data.ImportResult>> ImportTranslation([FromBody] TranslationImportRequest request)
    {
        try
        {
            var filePath = Path.Combine(_environment.ContentRootPath, request.FilePath);
            var result = await _translationImporter.ImportTranslationAsync(
                filePath,
                request.ModuleId,
                request.Language,
                request.Translator
            );

            if (result.Success)
            {
                _logger.LogInformation("Translation imported successfully: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Failed to import translation: {Message}", result.Message);
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during translation import");
            return StatusCode(500, new Data.Data.ImportResult
            {
                Success = false,
                Message = "Internal server error during import"
            });
        }
    }

    /// <summary>
    /// Import a book module
    /// </summary>
    [HttpPost("import/book")]
    public async Task<ActionResult<Data.Data.ImportResult>> ImportBook([FromBody] BookImportRequest request)
    {
        try
        {
            var filePath = Path.Combine(_environment.ContentRootPath, request.FilePath);
            var result = await _bookImporter.ImportBookAsync(filePath, request.ModuleId);

            if (result.Success)
            {
                _logger.LogInformation("Book imported successfully: {Message}", result.Message);
                return Ok(result);
            }
            else
            {
                _logger.LogError("Failed to import book: {Message}", result.Message);
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during book import");
            return StatusCode(500, new Data.Data.ImportResult
            {
                Success = false,
                Message = "Internal server error during import"
            });
        }
    }

    /// <summary>
    /// Mark a module as installed
    /// </summary>
    [HttpPost("{moduleId}/install")]
    public async Task<ActionResult> InstallModule(string moduleId)
    {
        try
        {
            var success = await _moduleService.MarkModuleInstalledAsync(moduleId);
            if (!success)
            {
                return NotFound($"Module {moduleId} not found");
            }
            return Ok(new { message = $"Module {moduleId} marked as installed" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error installing module");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Uninstall a module
    /// </summary>
    [HttpPost("{moduleId}/uninstall")]
    public async Task<ActionResult> UninstallModule(string moduleId)
    {
        try
        {
            var success = await _moduleService.UninstallModuleAsync(moduleId);
            if (!success)
            {
                return NotFound($"Module {moduleId} not found or not installed");
            }
            return Ok(new { message = $"Module {moduleId} uninstalled" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uninstalling module");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Delete a module
    /// </summary>
    [HttpDelete("{moduleId}")]
    public async Task<ActionResult> DeleteModule(string moduleId)
    {
        try
        {
            var success = await _moduleService.DeleteModuleAsync(moduleId);
            if (!success)
            {
                return NotFound($"Module {moduleId} not found");
            }
            return Ok(new { message = $"Module {moduleId} deleted" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting module");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Update module metadata
    /// </summary>
    [HttpPatch("{moduleId}")]
    public async Task<ActionResult> UpdateModule(string moduleId, [FromBody] ModuleUpdateData data)
    {
        try
        {
            var success = await _moduleService.UpdateModuleAsync(moduleId, data);
            if (!success)
            {
                return NotFound($"Module {moduleId} not found");
            }
            return Ok(new { message = $"Module {moduleId} updated" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating module");
            return StatusCode(500, "Internal server error");
        }
    }
}

public class TranslationImportRequest
{
    public string FilePath { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Translator { get; set; } = string.Empty;
}

public class BookImportRequest
{
    public string FilePath { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
}
