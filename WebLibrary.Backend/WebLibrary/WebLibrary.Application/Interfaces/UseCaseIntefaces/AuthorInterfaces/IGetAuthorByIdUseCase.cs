using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Получение автора по идентификатору.
/// </summary>
public interface IGetAuthorByIdUseCase
{
    Task<AuthorDto?> ExecuteAsync(Guid id);
}
