using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Получение всех авторов.
/// </summary>
public interface IGetAllAuthorsUseCase
{
    Task<IEnumerable<AuthorDto>> ExecuteAsync();
}