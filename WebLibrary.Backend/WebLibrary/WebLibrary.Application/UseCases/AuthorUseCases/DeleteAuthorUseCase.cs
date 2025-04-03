using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.AuthorUseCases;

    /// <summary>
    /// Кейс использования для удаления автора.
    /// </summary>
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IAuthorRepository _authorRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteAuthorUseCase"/>.
        /// </summary>
        /// <param name="authorRepository">Репозиторий авторов.</param>
        public DeleteAuthorUseCase(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Удаляет автора по указанному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор автора.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <exception cref="NotFoundException">Выбрасывается, если автор с указанным идентификатором не найден.</exception>
        public async Task ExecuteAsync(Guid id)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                throw new NotFoundException("Author with this id not found");
            }
            await _authorRepository.DeleteAsync(id);
        }
    }
