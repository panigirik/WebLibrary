using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для получения всех книг.
    /// </summary>
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAllBooksUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Сервис для преобразования данных.</param>
        public GetAllBooksUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет получение всех книг.
        /// </summary>
        /// <returns>Список всех книг, преобразованный в <see cref="GetBookRequestDto"/>.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> ExecuteAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }
    }
