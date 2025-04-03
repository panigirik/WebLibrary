using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Обновление автора по идентификатору.
/// </summary>
public interface IUpdateAuthorUseCase
{
    Task ExecuteAsync(AuthorDto authorDto);
}