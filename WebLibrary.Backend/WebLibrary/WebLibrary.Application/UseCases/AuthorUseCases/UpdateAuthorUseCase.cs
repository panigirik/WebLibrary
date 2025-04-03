using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для обновления данных автора.
    /// </summary>
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UpdateAuthorUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        /// <param name="mapper">Сервис маппинга.</param>
        public UpdateAuthorUseCase(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Выполняет обновление информации об авторе.
        /// </summary>
        /// <param name="authorDto">Объект <see cref="AuthorDto"/>, содержащий обновленные данные автора.</param>
        /// <exception cref="NotFoundException">Выбрасывается, если автор с указанным идентификатором не найден.</exception>
        public async Task ExecuteAsync(AuthorDto authorDto)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.AuthorId);
            if (existingAuthor == null)
            {
                throw new NotFoundException("Author with this id not found");
            }

            var author = _mapper.Map<Author>(authorDto);
            await _authorRepository.UpdateAsync(author);
        }
    }
