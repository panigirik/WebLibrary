using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для получения всех авторов.
    /// </summary>
    public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAllAuthorsUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        /// <param name="mapper">Сервис маппинга.</param>
        public GetAllAuthorsUseCase(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех авторов.
        /// </summary>
        /// <returns>Список авторов в виде коллекции <see cref="AuthorDto"/>.</returns>
        public async Task<IEnumerable<AuthorDto>> ExecuteAsync()
        {
            var authors =  _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }
    }
