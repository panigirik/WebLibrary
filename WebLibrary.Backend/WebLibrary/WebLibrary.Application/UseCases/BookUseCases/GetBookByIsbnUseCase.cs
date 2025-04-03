using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения книги по ISBN.
    /// </summary>
    public class GetBookByIsbnUseCase : IGetBookByIsbnUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBookByIsbnUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Сервис для преобразования данных.</param>
        public GetBookByIsbnUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает книгу по ISBN.
        /// </summary>
        /// <param name="isbn">ISBN книги.</param>
        /// <returns>DTO книги, если книга найдена, иначе выбрасывается исключение <see cref="NotFoundException"/>.</returns>
        /// <exception cref="NotFoundException">Если книга с данным ISBN не найдена.</exception>
        public async Task<GetBookRequestDto?> ExecuteAsync(string isbn)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);
            if (book == null)
            {
                throw new NotFoundException("Book not found.");
            }
            return _mapper.Map<GetBookRequestDto?>(book);
        }
    }
