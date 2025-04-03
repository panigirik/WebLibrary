using WebLibrary.Application.Dtos;

namespace WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;

public interface IGetBookByIdUseCase
{
    Task<GetBookRequestDto?> ExecuteAsync(Guid id);
}
