using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Caching.Distributed;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
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
        private readonly IBookValidationService _bookValidationService;
        private readonly IImageService _imageService;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BookService"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для отображения данных между сущностями и DTO.</param>
        public BookService(IBookRepository bookRepository,
            IMapper mapper,
            IBookValidationService bookValidationService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _bookValidationService = bookValidationService;
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
            if (book == null)
            {
                throw new NotFoundException("Book not found.");
            }
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
        /// <param name="bookRequest">Запрос с данными книги.</param>
        public async Task AddBookAsync(AddBookRequest bookRequest)
        {
            var existingBook = await _bookRepository.GetByIsbnAsync(bookRequest.ISBN);
            if (existingBook != null)
            {
                throw new ValidationException("book with this ISBN already exists.");
            }

            var book = _mapper.Map<Book>(bookRequest);
            book.BookId = Guid.NewGuid();

            if (bookRequest.ImageFile != null)
            {
                book.ImageData = await _imageService.ProcessAndStoreImageAsync(bookRequest.ImageFile);
            }

            await _bookValidationService.ValidateBookAsync(bookRequest, bookRequest.ImageFile);
            await _bookRepository.AddAsync(book);
        }



        /// <summary>
        /// Получает изображение книги.
        /// </summary>
        /// <param name="bookId">Уникальный идентификатор книги.</param>
        /// <returns>Массив байтов изображения книги или null, если изображение не найдено.</returns>
        public async Task<byte[]?> GetBookImageAsync(Guid bookId)
        {
            var imageData = await _imageService.GetImageAsync(bookId);
            if (imageData == null)
            {
                throw new NotFoundException("Book image not found.");
            }
            
            return imageData;
        }

        /// <summary>
        /// Обновляет данные книги.
        /// </summary>
        /// <param name="bookDto">DTO с обновленными данными книги.</param>
        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookDto.BookId);
            if (existingBook == null)
            {
                throw new ValidationException("book with this id not found");
            }
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.UpdateAsync(book);
        }

        /// <summary>
        /// Удаляет книгу по уникальному идентификатору.
        /// </summary>
        /// <param name="bookId">Уникальный идентификатор книги.</param>
        public async Task DeleteBookAsync(Guid bookId)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new ValidationException("book with this id not found");
            }
            await _bookRepository.DeleteAsync(bookId);
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
            {
                throw new NotFoundException("book not found");
            }
            var existingBook = await _bookRepository.GetByIdAsync(bookId);    
            if (existingBook == null)
            {
                throw new ValidationException("book with this id not found");
            }
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
            var existingBook = await _bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new ValidationException("book with this id not found");
            }
            book.IsAvailable = true;
            book.BorrowedById = null;
            book.BorrowedAt = null;
            book.ReturnBy = null;

            await _bookRepository.UpdateAsync(book);
            return true;
        }
    }

