using Microsoft.EntityFrameworkCore;
using AlHidayahPro.Data;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Services;

/// <summary>
/// Service implementation for Audio Recitation operations
/// </summary>
public class AudioService : IAudioService
{
    private readonly IslamicDbContext _context;

    public AudioService(IslamicDbContext context)
    {
        _context = context;
    }

    public async Task<AudioRecitationDto?> GetRecitationAsync(string reciterName, int surahNumber, int? ayahNumber = null)
    {
        var query = _context.AudioRecitations
            .Where(a => a.ReciterName.ToLower() == reciterName.ToLower() && 
                       a.SurahNumber == surahNumber);

        if (ayahNumber.HasValue)
        {
            query = query.Where(a => a.AyahNumber == ayahNumber.Value);
        }
        else
        {
            query = query.Where(a => a.AyahNumber == null);
        }

        var recitation = await query.FirstOrDefaultAsync();

        if (recitation == null) return null;

        return new AudioRecitationDto
        {
            ReciterName = recitation.ReciterName,
            SurahNumber = recitation.SurahNumber,
            AyahNumber = recitation.AyahNumber,
            AudioUrl = recitation.AudioUrl,
            Format = recitation.Format,
            DurationSeconds = recitation.DurationSeconds,
            RecitationStyle = recitation.RecitationStyle
        };
    }

    public async Task<List<AudioRecitationDto>> GetRecitationsBySurahAsync(string reciterName, int surahNumber)
    {
        var recitations = await _context.AudioRecitations
            .Where(a => a.ReciterName.ToLower() == reciterName.ToLower() && 
                       a.SurahNumber == surahNumber)
            .OrderBy(a => a.AyahNumber ?? 0)
            .ToListAsync();

        return recitations.Select(r => new AudioRecitationDto
        {
            ReciterName = r.ReciterName,
            SurahNumber = r.SurahNumber,
            AyahNumber = r.AyahNumber,
            AudioUrl = r.AudioUrl,
            Format = r.Format,
            DurationSeconds = r.DurationSeconds,
            RecitationStyle = r.RecitationStyle
        }).ToList();
    }

    public async Task<List<string>> GetRecitersAsync()
    {
        return await _context.AudioRecitations
            .Select(a => a.ReciterName)
            .Distinct()
            .OrderBy(r => r)
            .ToListAsync();
    }
}
