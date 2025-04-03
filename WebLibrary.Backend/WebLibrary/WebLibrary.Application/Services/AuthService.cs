using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IValidationService _validationService;

    public AuthService(IUserService userService, IJwtTokenService jwtTokenService, 
        IRefreshTokenService refreshTokenService, IValidationService validationService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _validationService = validationService;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var validationResult = await _validationService.ValidateLoginRequestAsync(request);
        if (!validationResult.IsValid)
            throw new UnauthorizedAccessException("Invalid login request");

        var user = await _userService.GetUserByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password");

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

        return new LoginResult(accessToken, refreshToken.Token);
    }


    public async Task<string> RefreshTokenAsync(Guid userId, string refreshToken)
    {
        var storedToken = await _refreshTokenService.GetRefreshTokenByUserIdAsync(userId);
        if (storedToken == null || storedToken.Token != refreshToken || storedToken.IsRevoked || storedToken.Expires < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        return _jwtTokenService.GenerateAccessToken(user.UserId, user.RoleType);
    }

    public async Task LogoutAsync(Guid userId)
    {
        await _refreshTokenService.RevokeRefreshTokenAsync(userId);
    }
}
