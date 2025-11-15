using Microsoft.AspNetCore.SignalR;

namespace AlHidayahPro.Api.Hubs;

/// <summary>
/// SignalR Hub for real-time study group features
/// </summary>
public class StudyHub : Hub
{
    private readonly ILogger<StudyHub> _logger;

    public StudyHub(ILogger<StudyHub> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Join a study group
    /// </summary>
    public async Task JoinStudyGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        _logger.LogInformation("User {ConnectionId} joined study group {GroupId}", Context.ConnectionId, groupId);
        
        // Notify other members
        await Clients.Group(groupId).SendAsync("UserJoined", Context.ConnectionId);
    }

    /// <summary>
    /// Leave a study group
    /// </summary>
    public async Task LeaveStudyGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        _logger.LogInformation("User {ConnectionId} left study group {GroupId}", Context.ConnectionId, groupId);
        
        // Notify other members
        await Clients.Group(groupId).SendAsync("UserLeft", Context.ConnectionId);
    }

    /// <summary>
    /// Share a verse with the study group
    /// </summary>
    public async Task ShareVerse(string groupId, int surahNumber, int ayahNumber, string? commentary = null)
    {
        _logger.LogInformation("User {ConnectionId} shared verse {Surah}:{Ayah} in group {GroupId}", 
            Context.ConnectionId, surahNumber, ayahNumber, groupId);
        
        await Clients.Group(groupId).SendAsync("VerseShared", new
        {
            SurahNumber = surahNumber,
            AyahNumber = ayahNumber,
            Commentary = commentary,
            SharedBy = Context.ConnectionId,
            Timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Synchronize reading position with the study group
    /// </summary>
    public async Task SyncReadingPosition(string groupId, int surahNumber, int ayahNumber)
    {
        _logger.LogInformation("User {ConnectionId} synced position {Surah}:{Ayah} in group {GroupId}", 
            Context.ConnectionId, surahNumber, ayahNumber, groupId);
        
        await Clients.Group(groupId).SendAsync("ReadingPositionUpdated", new
        {
            SurahNumber = surahNumber,
            AyahNumber = ayahNumber,
            UpdatedBy = Context.ConnectionId,
            Timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Send a message to the study group
    /// </summary>
    public async Task SendMessage(string groupId, string message)
    {
        _logger.LogInformation("User {ConnectionId} sent message in group {GroupId}", 
            Context.ConnectionId, groupId);
        
        await Clients.Group(groupId).SendAsync("MessageReceived", new
        {
            Message = message,
            SentBy = Context.ConnectionId,
            Timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Handle disconnection
    /// </summary>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("User {ConnectionId} disconnected", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
