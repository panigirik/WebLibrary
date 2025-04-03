using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Filters;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения списка книг с пагинацией.
    /// </summary>
    public class GetPaginatedBooksUseCase : IGetPaginatedBooksUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetPaginatedBooksUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для отображения объектов в DTO.</param>
        public GetPaginatedBooksUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список книг с пагинацией.
        /// </summary>
        /// <param name="filter">Фильтр с параметрами пагинации.</param>
        /// <returns>Список книг, соответствующих фильтру, представленных в виде DTO.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> ExecuteAsync(PaginatedBookFilter filter)
        {
            var books = await _bookRepository.GetPaginated(filter);
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }
    }
