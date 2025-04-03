using AutoMapper;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Exceptions;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Application.UseCases.NotificationsUseCases;

    /// <summary>
    /// Use-case для получения уведомления по идентификатору.
    /// </summary>
    public class GetNotificationByIdUseCase : IGetNotificationByIdUseCase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GetNotificationByIdUseCase"/>.
        /// </summary>
        /// <param name="notificationRepository">Репозиторий для работы с уведомлениями.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public GetNotificationByIdUseCase(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает уведомление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>DTO уведомления с указанным идентификатором.</returns>
        /// <exception cref="NotFoundException">Возникает, если уведомление с таким идентификатором не найдено.</exception>
        public async Task<NotificationDto?> ExecuteAsync(Guid id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification == null)
            {
                throw new NotFoundException("notification not found");
            }
            return _mapper.Map<NotificationDto?>(notification);
        }
    }
