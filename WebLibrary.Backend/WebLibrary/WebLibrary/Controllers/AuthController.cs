using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Requests;
using LoginRequest = WebLibrary.Application.Requests.LoginRequest;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для аутентификации пользователей через use-cases.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;
    private readonly ILogoutUseCase _logoutUseCase;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AuthController"/>.
    /// </summary>
    public AuthController(
        ILoginUseCase loginUseCase,
        IRefreshTokenUseCase refreshTokenUseCase,
        ILogoutUseCase logoutUseCase)
    {
        _loginUseCase = loginUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _logoutUseCase = logoutUseCase;
    }

    /// <summary>
    /// Выполняет вход пользователя, генерирует JWT токен.
    /// </summary>
    /// <param name="request">Запрос на вход с email и паролем.</param>
    /// <returns>Токен доступа и refresh токен.</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginRequest request)
    {
        var response = await _loginUseCase.ExecuteAsync(request);
        return Ok(response);
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
        var newAccessToken = await _refreshTokenUseCase.ExecuteAsync(userId, request.RefreshToken);
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
        Guid.TryParse(userIdClaim, out var userId);
        await _logoutUseCase.ExecuteAsync(userId);
        return NoContent();
    }
}
