using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;

namespace WebLibrary.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        // GET: api/notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        // GET: api/notifications/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDto>> GetNotificationById(Guid id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        // GET: api/notifications/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationDto>>> GetNotificationsByUserId(Guid userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        // POST: api/notifications
        [HttpPost]
        public async Task<ActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            await _notificationService.AddNotificationAsync(notificationDto);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notificationDto.NotificationId }, notificationDto);
        }

        // PUT: api/notifications/{id}/markAsRead
        [HttpPut("{id}/markAsRead")]
        public async Task<ActionResult> MarkAsRead(Guid id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return NoContent();
        }

        // DELETE: api/notifications/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNotification(Guid id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }

