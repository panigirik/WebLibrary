using System.ComponentModel.DataAnnotations.Schema;
using WebLibrary.Domain.Enums;

namespace WebLibrary.Application.Dtos;

/// <summary>
/// DTO (Data Transfer Object) для представления пользователя.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; } = "SampleUsername";

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; set; } = "samaplemail@gmail.com";

    /// <summary>
    /// Хэш пароля пользователя.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Роль пользователя в системе.
    /// </summary>
    public Roles RoleType { get; set; } = Roles.Admin;

    /// <summary>
    /// Список книг, взятых пользователем.
    /// </summary>
    public List<BookDto> BorrowedBooksIds { get; set; } = new();
}