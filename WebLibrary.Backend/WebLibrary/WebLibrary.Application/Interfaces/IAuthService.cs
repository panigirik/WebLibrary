using WebLibrary.Application.Requests;

namespace WebLibrary.Application.Interfaces;

public interface IAuthService
{
    public Task<LoginResult> LoginAsync(LoginRequest request);

    public Task<string> RefreshTokenAsync(Guid userId, string refreshToken);

    public Task LogoutAsync(Guid userId);
}