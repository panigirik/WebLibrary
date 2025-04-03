using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для отметки уведомления как прочитанного.
    /// </summary>
    public class MarkNotificationAsReadUseCase : IMarkNotificationAsReadUseCase
    {
        private readonly INotificationRepository _notificationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MarkNotificationAsReadUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        public MarkNotificationAsReadUseCase(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        /// <summary>
        /// Отмечает уведомление как прочитанное по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления, которое нужно отметить как прочитанное.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task ExecuteAsync(Guid id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateAsync(notification);
            }
        }
    }
