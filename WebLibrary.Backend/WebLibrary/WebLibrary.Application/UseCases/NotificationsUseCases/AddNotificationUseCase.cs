using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для добавления нового уведомления.
    /// </summary>
    public class AddNotificationUseCase : IAddNotificationUseCase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AddNotificationUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        /// <param name="mapper">Объект для преобразования данных между слоями.</param>
        public AddNotificationUseCase(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавляет новое уведомление в репозиторий.
        /// </summary>
        /// <param name="notificationDto">DTO объект, содержащий данные уведомления, которые нужно добавить.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task ExecuteAsync(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);
            await _notificationRepository.AddAsync(notification);
        }
    }
