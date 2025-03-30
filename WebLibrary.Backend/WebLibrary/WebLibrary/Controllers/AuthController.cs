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

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IUserService _userService;
    private readonly IValidationService _validationService;

    public AuthController(JwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService,
        IUserService userService, IValidationService validationService)
    {
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _userService = userService;
        _validationService = validationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // Сначала проверяем корректность входных данных
        var validationResult = await _validationService.ValidateLoginRequestAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        // Проверяем, существует ли пользователь с таким email
        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            // Возвращаем детализированное сообщение, если пользователь не найден
            return Unauthorized("Invalid email or password"); // В данном случае общее сообщение
        }
    
        // Логируем хэш пароля, чтобы удостовериться в его правильности
        Console.WriteLine($"Stored hash: {user.PasswordHash}");

        // Проверяем правильность пароля
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            // Возвращаем ошибку, если пароль неверный
            return Unauthorized("Invalid email or password");
        }

        // Генерация токенов после успешной авторизации
        var accessToken = _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user.UserId);

        // Сохраняем refresh token в базе данных
        await _refreshTokenService.AddRefreshTokenAsync(new RefreshTokenDto
        {
            RefreshTokenId = refreshToken.RefreshTokenId,
            UserId = user.UserId,
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            IsRevoked = refreshToken.IsRevoked
        });

        // Возвращаем токены пользователю
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken.Token });
    }



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



    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _refreshTokenService.RevokeRefreshTokenAsync(userId);
        return Ok("Logged out successfully");
    }
}
