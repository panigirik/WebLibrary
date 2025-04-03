using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для обновления книги.
    /// </summary>
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий для работы с книгами.</param>
        /// <param name="mapper">Интерфейс для маппинга объектов.</param>
        public UpdateBookUseCase(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Обновляет данные книги.
        /// </summary>
        /// <param name="updateBookRequest">Запрос на обновление информации о книге.</param>
        /// <exception cref="ValidationException">Выбрасывается, если книга с указанным идентификатором не найдена.</exception>
        /// <returns>Асинхронная задача.</returns>
        public async Task ExecuteAsync(UpdateBookRequest updateBookRequest)
        {
            var existingBook = await _bookRepository.GetByIdAsync(updateBookRequest.BookId);
            if (existingBook == null)
            {
                throw new ValidationException("Book with this ID not found");
            }
            var book = _mapper.Map<Book>(updateBookRequest);
            await _bookRepository.UpdateAsync(book);
        }
    }
