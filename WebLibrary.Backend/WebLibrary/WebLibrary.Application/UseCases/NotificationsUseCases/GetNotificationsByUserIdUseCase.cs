using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для получения уведомлений пользователя.
    /// </summary>
    public class GetNotificationsByUserIdUseCase : IGetNotificationsByUserIdUseCase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetNotificationsByUserIdUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public GetNotificationsByUserIdUseCase(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает уведомления для указанного пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, для которого нужно получить уведомления.</param>
        /// <returns>Список DTO уведомлений пользователя.</returns>
        public async Task<IEnumerable<NotificationDto>> ExecuteAsync(Guid userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
        }
    }
