using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Application.Services;

/// <summary>
/// Сервис для генерации JWT-токенов и Refresh-токенов.
/// </summary>
public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор сервиса токенов.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Генерирует access-токен для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="role">Роль пользователя.</param>
    /// <returns>Строка с JWT access-токеном.</returns>
    public string GenerateAccessToken(Guid userId, Roles role)
    {
        Console.WriteLine($"Generating token for user {userId} with role {role}");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, role.ToString()), // Важно: роль должна быть строкой "AdminRole"
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(40),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Генерирует refresh-токен для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Объект RefreshToken.</returns>
    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[32];
        rng.GetBytes(randomBytes);
        return new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            UserId = userId,
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(7), // Refresh токен на 7 дней
            IsRevoked = false
        };
    }
}
