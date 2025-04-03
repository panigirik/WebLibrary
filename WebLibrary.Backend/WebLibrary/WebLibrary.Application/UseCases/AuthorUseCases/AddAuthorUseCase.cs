using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для добавления нового автора.
    /// </summary>
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddAuthorUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        /// <param name="mapper">Маппер для преобразования DTO в сущность.</param>
        public AddAuthorUseCase(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет нового автора в базу данных.
        /// </summary>
        /// <param name="authorDto">Объект DTO, содержащий информацию об авторе.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task ExecuteAsync(AuthorDto authorDto)
        {
            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.AddAsync(author);
        }
    }
