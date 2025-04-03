using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для получения информации об авторе по его идентификатору.
    /// </summary>
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAuthorByIdUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        /// <param name="mapper">Сервис маппинга.</param>
        public GetAuthorByIdUseCase(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает информацию об авторе по его идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор автора.</param>
        /// <returns>Объект <see cref="AuthorDto"/> с данными автора или <c>null</c>, если автор не найден.</returns>
        public async Task<AuthorDto?> ExecuteAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return _mapper.Map<AuthorDto?>(author);
        }
    }
