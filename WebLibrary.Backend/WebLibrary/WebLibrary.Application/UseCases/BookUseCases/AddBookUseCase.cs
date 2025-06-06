﻿using AutoMapper;
using FluentValidation;
using WebLibrary.Application.Interfaces.Cache;
using WebLibrary.Application.Interfaces.ServiceInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.BookUseCases;

    /// <summary>
    /// Use-case для добавления новой книги в библиотеку.
    /// </summary>
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IAddBookValidationService _useCaseValidation;
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddBookUseCase"/>.
        /// </summary>
        /// <param name="bookRepository">Репозиторий книг.</param>
        /// <param name="mapper">Сервис AutoMapper для преобразования объектов.</param>
        /// <param name="useCaseValidation">Use-case для валидации данных книги.</param>
        /// <param name="processAndStrore">Use-case для обработки и сохранения изображения книги.</param>
        public AddBookUseCase(IBookRepository bookRepository,
            IMapper mapper,
            IAddBookValidationService useCaseValidation,
            IImageService imageService,
            ICacheService cacheService)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _useCaseValidation = useCaseValidation;
            _imageService = imageService;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Выполняет добавление новой книги.
        /// </summary>
        /// <param name="bookRequest">Данные о книге для добавления.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="ValidationException">
        /// Выбрасывается, если книга с указанным ISBN уже существует.
        /// </exception>
        public async Task ExecuteAsync(AddBookRequest bookRequest)
        {
            var existingBook = await _bookRepository.GetByIsbnAsync(bookRequest.ISBN);
            if (existingBook != null)
            {
                throw new ValidationException("Book with this ISBN already exists.");
            }

            var book = _mapper.Map<Book>(bookRequest);
            book.BookId = Guid.NewGuid();

            if (bookRequest.ImageFile != null)
            {
                book.ImageData = await _imageService.ProcessImageAsync(bookRequest.ImageFile);
            }
            
            await _useCaseValidation.ExecuteAsync(bookRequest, bookRequest.ImageFile);
            await _cacheService.SetAsync($"book:{book.BookId}", book, TimeSpan.FromHours(1));
            await _bookRepository.AddAsync(book);
        }
    }

