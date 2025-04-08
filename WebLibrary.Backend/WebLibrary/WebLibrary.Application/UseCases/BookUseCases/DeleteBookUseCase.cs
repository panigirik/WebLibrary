using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для удаления книги.
    /// </summary>
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Репозиторий для работы с книгами.</param>
        public DeleteBookUseCase(IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет удаление книги по её идентификатору.
        /// </summary>
        /// <param name="bookDto">Dto книги, коготрую нужно удалить.</param>
        /// <exception cref="ValidationException">
        /// Выбрасывается, если книга с указанным идентификатором не найдена.
        /// </exception>
        public async Task ExecuteAsync(BookDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookDto.BookId);
            if (existingBook == null)
            {
                throw new ValidationException("Book with this id not found");
            }
            var book =  _mapper.Map<Book>(bookDto);
            await _bookRepository.DeleteAsync(book);
        }
    }
