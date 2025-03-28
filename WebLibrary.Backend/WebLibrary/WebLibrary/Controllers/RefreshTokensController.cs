using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;

namespace WebLibrary.Controllers;

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

        // GET: api/refreshtokens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefreshTokenDto>>> GetAllRefreshTokens()
        {
            var tokens = await _refreshTokenService.GetAllRefreshTokensAsync();
            return Ok(tokens);
        }

        // GET: api/refreshtokens/{id}
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

        // GET: api/refreshtokens/user/{userId}
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

        // POST: api/refreshtokens
        [HttpPost]
        public async Task<ActionResult> AddRefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            await _refreshTokenService.AddRefreshTokenAsync(refreshTokenDto);
            return CreatedAtAction(nameof(GetRefreshTokenById), new { id = refreshTokenDto.RefreshTokenId }, refreshTokenDto);
        }

        // PUT: api/refreshtokens/{id}/revoke
        [HttpPut("{id}/revoke")]
        public async Task<ActionResult> RevokeRefreshToken(Guid id)
        {
            await _refreshTokenService.RevokeRefreshTokenAsync(id);
            return NoContent();
        }

        // DELETE: api/refreshtokens/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRefreshToken(Guid id)
        {
            await _refreshTokenService.DeleteRefreshTokenAsync(id);
            return NoContent();
        }
    }