using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для получения всех уведомлений.
    /// </summary>
    public class GetAllNotificationsUseCase : IGetAllNotificationsUseCase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetAllNotificationsUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public GetAllNotificationsUseCase(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает все уведомления.
        /// </summary>
        /// <returns>Список DTO всех уведомлений.</returns>
        public async Task<IEnumerable<NotificationDto>> ExecuteAsync()
        {
            var notifications = await _notificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
    }
