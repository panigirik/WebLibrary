namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;

/// <summary>
/// Удаление автора по его индентифкатору.
/// </summary>
public interface IDeleteAuthorUseCase
{
    Task ExecuteAsync(Guid id);
}