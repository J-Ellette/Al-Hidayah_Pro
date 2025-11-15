using AlHidayahPro.Api.Services;
using AlHidayahPro.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlHidayahPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly ILogger<QuizController> _logger;

    public QuizController(
        IQuizService quizService,
        ILogger<QuizController> logger)
    {
        _quizService = quizService;
        _logger = logger;
    }

    /// <summary>
    /// Get all quizzes (optionally filtered)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(
        [FromQuery] string? category = null,
        [FromQuery] string? difficulty = null)
    {
        var quizzes = await _quizService.GetQuizzesAsync(category, difficulty);
        return Ok(quizzes);
    }

    /// <summary>
    /// Get a specific quiz
    /// </summary>
    [HttpGet("{quizId}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int quizId)
    {
        var quiz = await _quizService.GetQuizAsync(quizId);
        if (quiz == null) return NotFound();
        return Ok(quiz);
    }

    /// <summary>
    /// Start a quiz attempt
    /// </summary>
    [HttpPost("{quizId}/start")]
    public async Task<ActionResult<QuizAttempt>> StartQuiz(int quizId, [FromBody] StartQuizRequest request)
    {
        try
        {
            var attempt = await _quizService.StartQuizAsync(request.UserId, quizId);
            return Ok(attempt);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Submit quiz answers
    /// </summary>
    [HttpPost("attempts/{attemptId}/submit")]
    public async Task<ActionResult<QuizAttempt>> SubmitQuiz(int attemptId, [FromBody] SubmitQuizRequest request)
    {
        try
        {
            var attempt = await _quizService.SubmitQuizAsync(attemptId, request.Answers);
            return Ok(attempt);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Get user quiz history
    /// </summary>
    [HttpGet("history/{userId}")]
    public async Task<ActionResult<IEnumerable<QuizAttempt>>> GetUserHistory(int userId)
    {
        var history = await _quizService.GetUserQuizHistoryAsync(userId);
        return Ok(history);
    }
}

public class StartQuizRequest
{
    public int UserId { get; set; }
}

public class SubmitQuizRequest
{
    public Dictionary<int, string> Answers { get; set; } = new();
}
