using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Services;
using LoginRequest = WebLibrary.Application.Requests.LoginRequest;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для аутентификации пользователей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserService _userService;
    private readonly IValidationService _validationService;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AuthController"/>.
    /// </summary>
    /// <param name="jwtTokenService">Сервис для работы с JWT токенами.</param>
    /// <param name="refreshTokenService">Сервис для работы с refresh токенами.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    /// <param name="validationService">Сервис для валидации данных.</param>
    public AuthController(JwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService,
        IUserService userService, IValidationService validationService)
    {
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _userService = userService;
        _validationService = validationService;
    }

    /// <summary>
    /// Выполняет вход пользователя, генерирует JWT токен.
    /// </summary>
    /// <param name="request">Запрос на вход с email и паролем.</param>
    /// <returns>Токен доступа и refresh токен.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var validationResult = await _validationService.ValidateLoginRequestAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

       var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }
    
        Console.WriteLine($"Stored hash: {user.PasswordHash}"); ///////////////////

        
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
           return Unauthorized("Invalid email or password");
        }

        
        var accessToken = _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user.UserId);

        await _refreshTokenService.AddRefreshTokenAsync(new RefreshTokenDto
        {
            RefreshTokenId = refreshToken.RefreshTokenId,
            UserId = user.UserId,
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            IsRevoked = refreshToken.IsRevoked
        });
        
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken.Token });
    }


    /// <summary>
    /// Обновляет access токен с использованием refresh токена.
    /// </summary>
    /// <param name="request">Запрос с refresh токеном.</param>
    /// <returns>Новый access токен.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID");
        }

        var storedToken = await _refreshTokenService.GetRefreshTokenByUserIdAsync(userId);
        if (storedToken == null || storedToken.Token != request.RefreshToken || storedToken.IsRevoked ||
            storedToken.Expires < DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token");

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            return Unauthorized("User not found");

        var accessToken = _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);

        return Ok(new { AccessToken = accessToken });
    }


    /// <summary>
    /// Выход из системы, отзывается refresh токен.
    /// </summary>
    /// <returns>Сообщение об успешном выходе.</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _refreshTokenService.RevokeRefreshTokenAsync(userId);
        return Ok("Logged out successfully");
    }
}
