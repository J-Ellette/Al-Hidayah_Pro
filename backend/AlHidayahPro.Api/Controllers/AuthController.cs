using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlHidayahPro.Api.Services;
using AlHidayahPro.Api.DTOs;

namespace AlHidayahPro.Api.Controllers;

/// <summary>
/// Authentication controller for user login and registration
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> _logger)
    {
        _authService = authService;
        this._logger = _logger;
    }

    /// <summary>
    /// User login endpoint
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username and password are required");
            }

            var response = await _authService.LoginAsync(request);
            
            if (response == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// User registration endpoint
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Username) || 
                string.IsNullOrWhiteSpace(request.Email) || 
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Username, email, and password are required");
            }

            if (request.Password.Length < 8)
            {
                return BadRequest("Password must be at least 8 characters long");
            }

            var response = await _authService.RegisterAsync(request);
            
            if (response == null)
            {
                return BadRequest("Username or email already exists");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Validate token endpoint
    /// </summary>
    [HttpPost("validate")]
    [Authorize]
    public async Task<ActionResult> ValidateToken()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var isValid = await _authService.ValidateTokenAsync(token);
            
            if (isValid)
            {
                return Ok(new { valid = true });
            }
            
            return Unauthorized(new { valid = false });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token validation");
            return StatusCode(500, "Internal server error");
        }
    }
}
