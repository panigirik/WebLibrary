using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Добавление нового автора.
/// </summary>
public interface IAddAuthorUseCase
{
    Task ExecuteAsync(AuthorDto authorDto);
}