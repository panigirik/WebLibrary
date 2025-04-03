using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

/// <summary>
/// Сервис для управления refresh-токенами.
/// </summary>
public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="RefreshTokenService"/>.
    /// </summary>
    /// <param name="refreshTokenRepository">Репозиторий refresh-токенов.</param>
    /// <param name="mapper">Маппер объектов.</param>
    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Получает все refresh-токены.
    /// </summary>
    /// <returns>Список refresh-токенов.</returns>
    public async Task<IEnumerable<RefreshTokenDto>> GetAllRefreshTokensAsync()
    {
        var tokens = await _refreshTokenRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RefreshTokenDto>>(tokens);
    }

    /// <summary>
    /// Получает refresh-токен по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор токена.</param>
    /// <returns>DTO refresh-токена или null.</returns>
    public async Task<RefreshTokenDto?> GetRefreshTokenByIdAsync(Guid id)
    {
        var token = await _refreshTokenRepository.GetByIdAsync(id);
        if (token == null)
        {
            throw new NotFoundException("token not found");
        }
        return _mapper.Map<RefreshTokenDto?>(token);
    }

    /// <summary>
    /// Получает refresh-токен по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>DTO refresh-токена или null.</returns>
    public async Task<RefreshTokenDto?> GetRefreshTokenByUserIdAsync(Guid userId)
    {
        var token = await _refreshTokenRepository.GetByUserIdAsync(userId);
        if (token == null)
        {
            throw new NotFoundException("token not found");
        }
        return _mapper.Map<RefreshTokenDto?>(token);
    }

    /// <summary>
    /// Добавляет новый refresh-токен.
    /// </summary>
    /// <param name="refreshTokenDto">DTO refresh-токена.</param>
    public async Task AddRefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
        await _refreshTokenRepository.AddAsync(refreshToken);
    }

    /// <summary>
    /// Отзывает refresh-токен по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор токена.</param>
    public async Task RevokeRefreshTokenAsync(Guid id)
    {
        var token = await _refreshTokenRepository.GetByIdAsync(id);
        if (token != null)
        {
            token.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(token);
        }
    }

    /// <summary>
    /// Удаляет refresh-токен по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор токена.</param>
    public async Task DeleteRefreshTokenAsync(Guid id)
    {
        await _refreshTokenRepository.DeleteAsync(id);
    }
}
