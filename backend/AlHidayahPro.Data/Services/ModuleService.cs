using System.Text.Json;
using AlHidayahPro.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlHidayahPro.Data.Services;

/// <summary>
/// Service for managing content modules (Quran translations, Hadiths, Books)
/// </summary>
public class ModuleService
{
    private readonly IslamicDbContext _context;

    public ModuleService(IslamicDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all available modules
    /// </summary>
    public async Task<List<ContentModule>> GetAllModulesAsync()
    {
        return await _context.ContentModules
            .OrderBy(m => m.Type)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Get installed modules
    /// </summary>
    public async Task<List<ContentModule>> GetInstalledModulesAsync()
    {
        return await _context.ContentModules
            .Where(m => m.IsInstalled)
            .OrderBy(m => m.Type)
            .ThenBy(m => m.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Get modules by type
    /// </summary>
    public async Task<List<ContentModule>> GetModulesByTypeAsync(ModuleType type)
    {
        return await _context.ContentModules
            .Where(m => m.Type == type)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Register a new module
    /// </summary>
    public async Task<ContentModule> RegisterModuleAsync(ModuleRegistrationData data)
    {
        var existingModule = await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == data.ModuleId);

        if (existingModule != null)
        {
            throw new InvalidOperationException($"Module {data.ModuleId} is already registered");
        }

        var module = new ContentModule
        {
            ModuleId = data.ModuleId,
            Name = data.Name,
            Type = data.Type,
            Language = data.Language,
            Author = data.Author,
            Description = data.Description,
            Version = data.Version,
            SourceUrl = data.SourceUrl,
            FileSize = data.FileSize,
            License = data.License,
            Metadata = data.Metadata,
            IsInstalled = false
        };

        _context.ContentModules.Add(module);
        await _context.SaveChangesAsync();

        return module;
    }

    /// <summary>
    /// Mark module as installed
    /// </summary>
    public async Task<bool> MarkModuleInstalledAsync(string moduleId)
    {
        var module = await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

        if (module == null)
            return false;

        module.IsInstalled = true;
        module.InstalledDate = DateTime.UtcNow;
        module.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Uninstall a module
    /// </summary>
    public async Task<bool> UninstallModuleAsync(string moduleId)
    {
        var module = await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

        if (module == null || !module.IsInstalled)
            return false;

        module.IsInstalled = false;
        module.InstalledDate = null;
        module.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Delete a module completely
    /// </summary>
    public async Task<bool> DeleteModuleAsync(string moduleId)
    {
        var module = await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

        if (module == null)
            return false;

        _context.ContentModules.Remove(module);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Get module by ID
    /// </summary>
    public async Task<ContentModule?> GetModuleAsync(string moduleId)
    {
        return await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == moduleId);
    }

    /// <summary>
    /// Update module metadata
    /// </summary>
    public async Task<bool> UpdateModuleAsync(string moduleId, ModuleUpdateData data)
    {
        var module = await _context.ContentModules
            .FirstOrDefaultAsync(m => m.ModuleId == moduleId);

        if (module == null)
            return false;

        if (!string.IsNullOrEmpty(data.Name))
            module.Name = data.Name;
        
        if (!string.IsNullOrEmpty(data.Description))
            module.Description = data.Description;
        
        if (!string.IsNullOrEmpty(data.Version))
            module.Version = data.Version;

        module.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}

/// <summary>
/// Data for registering a new module
/// </summary>
public class ModuleRegistrationData
{
    public string ModuleId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ModuleType Type { get; set; }
    public string Language { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Description { get; set; }
    public string Version { get; set; } = "1.0.0";
    public string? SourceUrl { get; set; }
    public long FileSize { get; set; }
    public string? License { get; set; }
    public string? Metadata { get; set; }
}

/// <summary>
/// Data for updating a module
/// </summary>
public class ModuleUpdateData
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
}
