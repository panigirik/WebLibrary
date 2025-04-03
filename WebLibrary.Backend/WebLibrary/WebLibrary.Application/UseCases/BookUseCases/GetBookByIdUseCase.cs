using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения книги по идентификатору.
    /// </summary>
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBookByIdUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Сервис для преобразования данных.</param>
        public GetBookByIdUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает книгу по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор книги.</param>
        /// <returns>DTO книги, если книга найдена, иначе <c>null</c>.</returns>
        public async Task<GetBookRequestDto?> ExecuteAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<GetBookRequestDto?>(book);
        }
    }
