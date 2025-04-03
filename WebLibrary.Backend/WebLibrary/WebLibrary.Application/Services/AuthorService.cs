using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

    /// <summary>
    /// Сервис для управления авторами.
    /// </summary>
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AuthorService"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий для работы с авторами.</param>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для отображения данных между сущностями и DTO.</param>
        public AuthorService(IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает всех авторов.
        /// </summary>
        /// <returns>Список DTO авторов.</returns>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors =  _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// Получает автора по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор автора.</param>
        /// <returns>DTO автора или null, если автор не найден.</returns>
        public async Task<AuthorDto?> GetAuthorByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return _mapper.Map<AuthorDto?>(author);
        }

        /// <summary>
        /// Добавляет нового автора.
        /// </summary>
        /// <param name="authorDto">DTO с данными автора.</param>
        public async Task AddAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.AddAsync(author);
        }

        /// <summary>
        /// Обновляет существующего автора.
        /// </summary>
        /// <param name="authorDto">DTO с обновленными данными автора.</param>
        public async Task UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.AuthorId);
            if (existingAuthor == null)
            {
                throw new NotFoundException("authir with this id not found");
            }
            await _authorRepository.UpdateAsync(author);
        }

        /// <summary>
        /// Удаляет автора по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор автора.</param>
        public async Task DeleteAuthorAsync(Guid id)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                throw new NotFoundException("authir with this id not found");
            }
            await _authorRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Получает все книги, написанные автором.
        /// </summary>
        /// <param name="authorId">Уникальный идентификатор автора.</param>
        /// <returns>Список DTO книг, написанных автором.</returns>
        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }

