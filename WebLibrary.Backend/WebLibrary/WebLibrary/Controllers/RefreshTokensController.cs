using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;



namespace WebLibrary.Controllers;

    /// <summary>
    /// Контроллер для управления refresh-токенами.
    /// <remarks>Контроллер для наглядности.</remarks>>
    ///</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokensController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public RefreshTokensController(IRefreshTokenService refreshTokenService, IMapper mapper)
        {
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RefreshTokensController"/>.
        /// </summary>
        /// <param name="refreshTokenService">Сервис для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования данных.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshTokenDto>>> GetAllRefreshTokens()
        {
            var tokens = await _refreshTokenService.GetAllRefreshTokensAsync();
            return Ok(tokens);
        }

        /// <summary>
        /// Получить refresh-токен по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена.</param>
        /// <returns>Refresh-токен с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RefreshTokenDto>> GetRefreshTokenById(Guid id)
        {
            var token = await _refreshTokenService.GetRefreshTokenByIdAsync(id);
            if (token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }

        /// <summary>
        /// Получить refresh-токен по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Refresh-токен пользователя.</returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<RefreshTokenDto>> GetRefreshTokenByUserId(Guid userId)
        {
            var token = await _refreshTokenService.GetRefreshTokenByUserIdAsync(userId);
            if (token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }

        /// <summary>
        /// Добавить новый refresh-токен.
        /// </summary>
        /// <param name="refreshTokenDto">Данные для создания нового refresh-токена.</param>
        /// <returns>Результат операции добавления refresh-токена.</returns>
        [HttpPost]
        public async Task<ActionResult> AddRefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            await _refreshTokenService.AddRefreshTokenAsync(refreshTokenDto);
            return CreatedAtAction(nameof(GetRefreshTokenById), new { id = refreshTokenDto.RefreshTokenId }, refreshTokenDto);
        }

        /// <summary>
        /// Отозвать refresh-токен.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена.</param>
        /// <returns>Результат операции отзыва токена.</returns>
        [HttpPut("{id}/revoke")]
        public async Task<ActionResult> RevokeRefreshToken(Guid id)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Удалить refresh-токен.
        /// </summary>
        /// <param name="id">Идентификатор refresh-токена.</param>
        /// <returns>Результат операции удаления.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRefreshToken(Guid id)
        {
            await _refreshTokenService.DeleteRefreshTokenAsync(id);
            return NoContent();
        }
    }