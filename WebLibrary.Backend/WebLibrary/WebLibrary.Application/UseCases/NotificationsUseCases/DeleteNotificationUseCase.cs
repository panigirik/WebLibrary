using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для удаления уведомления.
    /// </summary>
    public class DeleteNotificationUseCase : IDeleteNotificationUseCase
    {
        private readonly INotificationRepository _notificationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteNotificationUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        public DeleteNotificationUseCase(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Удаляет уведомление по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления, которое нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления уведомления.</returns>
        public async Task ExecuteAsync(Guid id)
        {
            await _notificationRepository.DeleteAsync(id);
        }
    }
