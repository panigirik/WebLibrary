using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Filters;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.Services;

    /// <summary>
    /// Сервис для работы с книгами.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookService"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для отображения данных между сущностями и DTO.</param>
        public BookService(IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает все книги.
        /// </summary>
        /// <returns>Список DTO книг.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }

        /// <summary>
        /// Получает книгу по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги.</param>
        /// <returns>DTO книги или null, если книга не найдена.</returns>
        public async Task<GetBookRequestDto?> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<GetBookRequestDto?>(book);
        }

        /// <summary>
        /// Получает книги с постраничной навигацией.
        /// </summary>
        /// <param name="filter">Параметры фильтрации и пагинации.</param>
        /// <returns>Список DTO книг.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> GetPaginatedBooksAsync(PaginatedBookFilter filter)
        {
            var books = await _bookRepository.GetPaginated(filter);
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }

        /// <summary>
        /// Получает книгу по ISBN.
        /// </summary>
        /// <param name="isbn">ISBN книги.</param>
        /// <returns>DTO книги или null, если книга не найдена.</returns>
        public async Task<GetBookRequestDto?> GetBookByIsbnAsync(string isbn)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);
            return _mapper.Map<GetBookRequestDto?>(book);
        }

        /// <summary>
        /// Получает все книги автора.
        /// </summary>
        /// <param name="authorId">Уникальный идентификатор автора.</param>
        /// <returns>Список DTO книг автора.</returns>
        public async Task<IEnumerable<GetBookRequestDto>> GetBooksByAuthorAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<GetBookRequestDto>>(books);
        }

        /// <summary>
        /// Добавляет новую книгу.
        /// </summary>
        /// <param name="bookDto">DTO с данными книги.</param>
        public async Task AddBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                BookId = Guid.NewGuid(),
                ISBN = bookDto.ISBN,
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                Description = bookDto.Description,
                AuthorId = bookDto.AuthorId,
                BorrowedAt = bookDto.BorrowedAt,
                ReturnBy = bookDto.ReturnBy,
                BorrowedById = bookDto.BorrowedById,
                IsAvailable = bookDto.IsAvailable
            };

            if (bookDto.ImageFile != null)
            {
                using var memoryStream = new MemoryStream();
                await bookDto.ImageFile.CopyToAsync(memoryStream);
                book.ImageData = memoryStream.ToArray();
            }

            await _bookRepository.AddAsync(book);
        }

        /// <summary>
        /// Получает изображение книги.
        /// </summary>
        /// <param name="bookId">Уникальный идентификатор книги.</param>
        /// <returns>Массив байтов изображения книги или null, если изображение не найдено.</returns>
        public async Task<byte[]?> GetBookImageAsync(Guid bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book?.ImageData != null)
            {
                return book.ImageData;
            }

            return null;
        }

        /// <summary>
        /// Обновляет данные книги.
        /// </summary>
        /// <param name="bookDto">DTO с обновленными данными книги.</param>
        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.UpdateAsync(book);
        }

        /// <summary>
        /// Удаляет книгу по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги.</param>
        public async Task DeleteBookAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Позволяет пользователю взять книгу на время.
        /// </summary>
        /// <param name="bookId">Уникальный идентификатор книги.</param>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>True, если книга успешно взята, иначе false.</returns>
        public async Task<bool> BorrowBookAsync(Guid bookId, Guid userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || !book.IsAvailable)
                return false;

            book.IsAvailable = false;
            book.BorrowedById = userId;
            book.BorrowedAt = DateTime.UtcNow;
            book.ReturnBy = DateTime.UtcNow.AddDays(14);

            await _bookRepository.UpdateAsync(book);
            return true;
        }

        /// <summary>
        /// Позволяет пользователю вернуть книгу.
        /// </summary>
        /// <param name="bookId">Уникальный идентификатор книги.</param>
        /// <param name="userId">Уникальный идентификатор пользователя.</param>
        /// <returns>True, если книга успешно возвращена, иначе false.</returns>
        public async Task<bool> ReturnBookAsync(Guid bookId, Guid userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null || book.BorrowedById != userId)
                return false;

            book.IsAvailable = true;
            book.BorrowedById = null;
            book.BorrowedAt = null;
            book.ReturnBy = null;

            await _bookRepository.UpdateAsync(book);
            return true;
        }
    }

