using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;

namespace WebLibrary.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/users/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> AddUser([FromForm] UserDto userDto)
        {
            await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserId }, userDto);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest("ID mismatch.");
            }

            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }