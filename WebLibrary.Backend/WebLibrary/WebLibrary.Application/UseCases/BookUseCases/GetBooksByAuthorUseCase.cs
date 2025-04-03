using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения списка книг по идентификатору автора.
    /// </summary>
    public class GetBooksByAuthorUseCase : IGetBooksByAuthorUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBooksByAuthorUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для отображения объектов в DTO.</param>
        public GetBooksByAuthorUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список книг по идентификатору автора.
        /// </summary>
        /// <param name="authorId">Идентификатор автора.</param>
        /// <returns>Список книг, принадлежащих автору, представленных в виде DTO.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> ExecuteAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }
    }
