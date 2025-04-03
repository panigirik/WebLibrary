using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;

namespace WebLibrary.Controllers;

    /// <summary>
    /// Контроллер для управления уведомлениями.
    /// <remarks>Контроллер для наглядности.</remarks>>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="NotificationsController"/>.
        /// </summary>
        /// <param name="notificationService">Сервис для работы с уведомлениями.</param>
        /// <param name="mapper">Маппер для преобразования данных.</param>
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Получить все уведомления.
        /// </summary>
        /// <returns>Список всех уведомлений.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
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
            var notification = await _notificationService.GetNotificationByIdAsync(id);
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
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
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
            await _notificationService.AddNotificationAsync(notificationDto);
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
            await _notificationService.MarkAsReadAsync(id);
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
            await _notificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }

