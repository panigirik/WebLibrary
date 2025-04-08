using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для удаления автора.
    /// </summary>
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteAuthorUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        /// <param name="mapper">MappingProfile для авторов.</param>
        public DeleteAuthorUseCase(IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Удаляет автора по указанному идентификатору.
        /// </summary>
        /// <param name="authorDto">Dto автора, которого надо удалить.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="NotFoundException">Выбрасывается, если автор с указанным идентификатором не найден.</exception>
        public async Task ExecuteAsync(AuthorDto authorDto)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.AuthorId);
            if (existingAuthor == null)
            {
                throw new NotFoundException("Author with this id not found");
            }

            var author =  _mapper.Map<Author>(authorDto);
            await _authorRepository.DeleteAsync(author);
        }
    }
