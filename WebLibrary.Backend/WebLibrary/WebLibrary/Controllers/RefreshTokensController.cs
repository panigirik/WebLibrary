using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;

namespace WebLibrary.Controllers
{
    /// <summary>
    /// Контроллер для управления refresh-токенами.
    /// <remarks>Контроллер для наглядности.</remarks>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokensController : ControllerBase
    {
        private readonly IGetAllRefreshTokensUseCase _getAllRefreshTokensUseCase;
        private readonly IGetRefreshTokenByIdUseCase _getRefreshTokenByIdUseCase;
        private readonly IGetRefreshTokenByUserIdUseCase _getRefreshTokenByUserIdUseCase;
        private readonly IAddRefreshTokenUseCase _addRefreshTokenUseCase;
        private readonly IRevokeRefreshTokenUseCase _revokeRefreshTokenUseCase;
        private readonly IDeleteRefreshTokenUseCase _deleteRefreshTokenUseCase;

        public RefreshTokensController(
            IGetAllRefreshTokensUseCase getAllRefreshTokensUseCase,
            IGetRefreshTokenByIdUseCase getRefreshTokenByIdUseCase,
            IGetRefreshTokenByUserIdUseCase getRefreshTokenByUserIdUseCase,
            IAddRefreshTokenUseCase addRefreshTokenUseCase,
            IRevokeRefreshTokenUseCase revokeRefreshTokenUseCase,
            IDeleteRefreshTokenUseCase deleteRefreshTokenUseCase)
        {
            _getAllRefreshTokensUseCase = getAllRefreshTokensUseCase;
            _getRefreshTokenByIdUseCase = getRefreshTokenByIdUseCase;
            _getRefreshTokenByUserIdUseCase = getRefreshTokenByUserIdUseCase;
            _addRefreshTokenUseCase = addRefreshTokenUseCase;
            _revokeRefreshTokenUseCase = revokeRefreshTokenUseCase;
            _deleteRefreshTokenUseCase = deleteRefreshTokenUseCase;
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RefreshTokensController"/>.
        /// </summary>
        /// <param name="refreshTokenService">Сервис для работы с refresh-токенами.</param>
        /// <param name="mapper">Маппер для преобразования данных.</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshTokenDto>>> GetAllRefreshTokens()
        {
            var tokens = await _getAllRefreshTokensUseCase.ExecuteAsync();
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
            var token = await _getRefreshTokenByIdUseCase.ExecuteAsync(id);
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
            var token = await _getRefreshTokenByUserIdUseCase.ExecuteAsync(userId);
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
            await _addRefreshTokenUseCase.ExecuteAsync(refreshTokenDto);
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
            await _revokeRefreshTokenUseCase.ExecuteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Удалить refresh-токен.
        /// </summary>
        /// <param name="refreshTokenDto">refresh-токен, который нужно удалить.</param>
        /// <returns>Результат операции удаления.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            await _deleteRefreshTokenUseCase.ExecuteAsync(refreshTokenDto);
            return NoContent();
        }
    }
}
