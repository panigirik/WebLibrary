using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using WebLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Requests;
using LoginRequest = WebLibrary.Application.Requests.LoginRequest;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для аутентификации пользователей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AuthController"/>.
    /// </summary>
    /// <param name="jwtTokenService">Сервис для работы с JWT токенами.</param>
    /// <param name="refreshTokenService">Сервис для работы с refresh токенами.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    /// <param name="validationService">Сервис для валидации данных.</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Выполняет вход пользователя, генерирует JWT токен.
    /// </summary>
    /// <param name="request">Запрос на вход с email и паролем.</param>
    /// <returns>Токен доступа и refresh токен.</returns>
    [HttpPost("login")]
    public async Task<LoginResult> Login([FromBody] LoginRequest request)
    {
        return await _authService.LoginAsync(request);
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
        Guid.TryParse(userIdClaim, out var userId);
        var newAccessToken = await _authService.RefreshTokenAsync(userId, request.RefreshToken);
        return Ok(new { AccessToken = newAccessToken });
    }



    /// <summary>
    /// Выход из системы, отзывается refresh токен.
    /// </summary>
    /// <returns>Сообщение об успешном выходе.</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdClaim, out var UserId);
        await _authService.LogoutAsync(UserId);
        return NoContent();
    }
}
