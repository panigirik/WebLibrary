using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;

namespace WebLibrary.Controllers
{
    /// <summary>
    /// Контроллер для управления уведомлениями.
    /// <remarks>Контроллер для наглядности.</remarks>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IGetAllNotificationsUseCase _getAllNotificationsUseCase;
        private readonly IGetNotificationByIdUseCase _getNotificationByIdUseCase;
        private readonly IGetNotificationsByUserIdUseCase _getNotificationsByUserIdUseCase;
        private readonly IAddNotificationUseCase _addNotificationUseCase;
        private readonly IMarkNotificationAsReadUseCase _markNotificationAsReadUseCase;
        private readonly IDeleteNotificationUseCase _deleteNotificationUseCase;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="NotificationsController"/>.
        /// </summary>
        /// <param name="getAllNotificationsUseCase">Use case для получения всех уведомлений.</param>
        /// <param name="getNotificationByIdUseCase">Use case для получения уведомления по идентификатору.</param>
        /// <param name="getNotificationsByUserIdUseCase">Use case для получения уведомлений пользователя.</param>
        /// <param name="addNotificationUseCase">Use case для добавления нового уведомления.</param>
        /// <param name="markNotificationAsReadUseCase">Use case для пометки уведомления как прочитанного.</param>
        /// <param name="deleteNotificationUseCase">Use case для удаления уведомления.</param>
        public NotificationsController(
            IGetAllNotificationsUseCase getAllNotificationsUseCase,
            IGetNotificationByIdUseCase getNotificationByIdUseCase,
            IGetNotificationsByUserIdUseCase getNotificationsByUserIdUseCase,
            IAddNotificationUseCase addNotificationUseCase,
            IMarkNotificationAsReadUseCase markNotificationAsReadUseCase,
            IDeleteNotificationUseCase deleteNotificationUseCase)
        {
            _getAllNotificationsUseCase = getAllNotificationsUseCase;
            _getNotificationByIdUseCase = getNotificationByIdUseCase;
            _getNotificationsByUserIdUseCase = getNotificationsByUserIdUseCase;
            _addNotificationUseCase = addNotificationUseCase;
            _markNotificationAsReadUseCase = markNotificationAsReadUseCase;
            _deleteNotificationUseCase = deleteNotificationUseCase;
        }

        /// <summary>
        /// Получить все уведомления.
        /// </summary>
        /// <returns>Список всех уведомлений.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAllNotifications()
        {
            var notifications = await _getAllNotificationsUseCase.ExecuteAsync();
            return Ok(notifications);
        }

        /// <summary>
        /// Получить уведомление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Уведомление с указанным идентификатором.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotificationById(Guid id)
        {
            var notification = await _getNotificationByIdUseCase.ExecuteAsync(id);
            return Ok(notification);
        }

        /// <summary>
        /// Получить уведомления пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список уведомлений пользователя.</returns>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId(Guid userId)
        {
            var notifications = await _getNotificationsByUserIdUseCase.ExecuteAsync(userId);
            return Ok(notifications);
        }

        /// <summary>
        /// Добавить новое уведомление.
        /// </summary>
        /// <param name="notificationDto">Данные для создания нового уведомления.</param>
        /// <returns>Результат операции добавления уведомления.</returns>
        [HttpPost]
        public async Task<ActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            await _addNotificationUseCase.ExecuteAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notificationDto.NotificationId }, notificationDto);
        }

        /// <summary>
        /// Пометить уведомление как прочитанное.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{id}/markAsRead")]
        public async Task<ActionResult> MarkAsRead(Guid id)
        {
            await _markNotificationAsReadUseCase.ExecuteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Удалить уведомление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор уведомления.</param>
        /// <returns>Результат операции удаления.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(Guid id)
        {
            await _deleteNotificationUseCase.ExecuteAsync(id);
            return NoContent();
        }
    }
}
