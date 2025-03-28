using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RefreshTokenDto>> GetAllRefreshTokensAsync()
    {
        var tokens = await _refreshTokenRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RefreshTokenDto>>(tokens);
    }

    public async Task<RefreshTokenDto?> GetRefreshTokenByIdAsync(Guid id)
    {
        var token = await _refreshTokenRepository.GetByIdAsync(id);
        return _mapper.Map<RefreshTokenDto?>(token);
    }

    public async Task<RefreshTokenDto?> GetRefreshTokenByUserIdAsync(Guid userId)
    {
        var token = await _refreshTokenRepository.GetByUserIdAsync(userId);
        return _mapper.Map<RefreshTokenDto?>(token);
    }

    public async Task AddRefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
        await _refreshTokenRepository.AddAsync(refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(Guid id)
    {
        var token = await _refreshTokenRepository.GetByIdAsync(id);
        if (token != null)
        {
            token.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(token);
        }
    }

    public async Task DeleteRefreshTokenAsync(Guid id)
    {
        await _refreshTokenRepository.DeleteAsync(id);
    }
}