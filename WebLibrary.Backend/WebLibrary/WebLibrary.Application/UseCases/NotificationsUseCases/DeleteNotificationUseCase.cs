using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для удаления уведомления.
    /// </summary>
    public class DeleteNotificationUseCase : IDeleteNotificationUseCase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeleteNotificationUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        /// <param name="mapper">Mapper для преобразования сущностей.</param>
        public DeleteNotificationUseCase(INotificationRepository notificationRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Удаляет уведомление по его идентификатору.
        /// </summary>
        /// <param name="notificationDto">Уведомление которое нужно удалить.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления уведомления.</returns>
        public async Task ExecuteAsync(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _notificationRepository.DeleteAsync(notification);
        }
    }
