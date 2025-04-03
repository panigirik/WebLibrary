using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Application.Requests;

namespace WebLibrary.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
    private readonly IAddUserUseCase _addUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IDeleteUserUseCase _deleteUserUseCase;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UsersController"/>.
    /// </summary>
    /// <param name="getAllUsersUseCase">Use case для получения всех пользователей.</param>
    /// <param name="getUserByIdUseCase">Use case для получения пользователя по идентификатору.</param>
    /// <param name="getUserByEmailUseCase">Use case для получения пользователя по email.</param>
    /// <param name="addUserUseCase">Use case для добавления нового пользователя.</param>
    /// <param name="updateUserUseCase">Use case для обновления информации о пользователе.</param>
    /// <param name="deleteUserUseCase">Use case для удаления пользователя.</param>
    public UsersController(
        IGetAllUsersUseCase getAllUsersUseCase,
        IGetUserByIdUseCase getUserByIdUseCase,
        IGetUserByEmailUseCase getUserByEmailUseCase,
        IAddUserUseCase addUserUseCase,
        IUpdateUserUseCase updateUserUseCase,
        IDeleteUserUseCase deleteUserUseCase)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByEmailUseCase = getUserByEmailUseCase;
        _addUserUseCase = addUserUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    /// <summary>
    /// Получить всех пользователей.
    /// </summary>
    /// <returns>Список всех пользователей.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _getAllUsersUseCase.ExecuteAsync();
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
        var user = await _getUserByIdUseCase.ExecuteAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Получить пользователя по email.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Пользователь с указанным email.</returns>
    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
    {
        var user = await _getUserByEmailUseCase.ExecuteAsync(email);
        return user == null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Добавить нового пользователя.
    /// </summary>
    /// <param name="userDto">Данные для создания нового пользователя.</param>
    /// <returns>Результат операции добавления пользователя.</returns>
    [HttpPost]
    public async Task<ActionResult> AddUser([FromForm] UserDto userDto)
    {
        await _addUserUseCase.ExecuteAsync(userDto);
        return CreatedAtAction(nameof(GetUserById), new { id = userDto.UserId }, userDto);
    }

    /// <summary>
    /// Редактировать пользователя.
    /// </summary>
    /// <param name="userDto">Данные для редактирования пользователя.</param>
    /// <returns>NoContent().</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserInfoRequest updateUserInfoRequest)
    {
        await _updateUserUseCase.ExecuteAsync(updateUserInfoRequest);
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
        await _deleteUserUseCase.ExecuteAsync(id);
        return NoContent();
    }
}
