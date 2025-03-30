using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Exceptions;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
   
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UsersController"/>.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
        }

    /// <summary>
    /// Получить всех пользователей.
    /// </summary>
    /// <returns>Список всех пользователей.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Пользователь с указанным идентификатором.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        return Ok(user);
    }

    /// <summary>
    /// Получить пользователя по email.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Пользователь с указанным email.</returns>
    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        return Ok(user);
    }

    /// <summary>
    /// Добавить нового пользователя.
    /// </summary>
    /// <param name="userDto">Данные для создания нового пользователя.</param>
    /// <returns>Результат операции добавления пользователя.</returns>
    [HttpPost]
    public async Task<ActionResult> AddUser([FromForm] UserDto userDto)
    {
        await _userService.AddUserAsync(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserId }, userDto);
    }

    /// <summary>
    /// Редактировать пользователя.
    /// </summary>
    /// <param name="userDto">Данные для редактирования пользователя.</param>
    /// <returns>NoContent().</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
    {
        if (id != userDto.UserId)
        {
            throw new BadRequestException("ID mismatch.");
        }

        await _userService.UpdateUserAsync(userDto);
        return NoContent();
    }

    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id">Guid для поиска пользователя.</param>
    /// <returns>NoContent().</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
