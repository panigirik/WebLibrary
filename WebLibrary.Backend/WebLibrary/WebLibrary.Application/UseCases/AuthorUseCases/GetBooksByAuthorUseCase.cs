using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для получения всех книг определенного автора.
    /// </summary>
    public class GetBooksAuthorUseCase : IGetBooksAuthorUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetBooksAuthorUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий книг.</param>
        /// <param name="mapper">Сервис маппинга.</param>
        public GetBooksAuthorUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех книг, написанных автором.
        /// </summary>
        /// <param name="authorId">Уникальный идентификатор автора.</param>
        /// <returns>Коллекция объектов <see cref="BookDto"/>, содержащих информацию о книгах автора.</returns>
        public async Task<IEnumerable<BookDto>> ExecuteAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
