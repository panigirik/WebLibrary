using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

/// <summary>
/// Репозиторий для работы с уведомлениями.
/// </summary>
public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="NotificationRepository"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает уведомление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    /// <returns>Уведомление с указанным идентификатором.</returns>
    public async Task<Notification> GetByIdAsync(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.UserId == id);
    }

    /// <summary>
    /// Получает все уведомления.
    /// </summary>
    /// <returns>Список всех уведомлений.</returns>
    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await _context.Notifications
            .Include(n => n.User)
            .ToListAsync();
    }

    /// <summary>
    /// Добавляет новое уведомление.
    /// </summary>
    /// <param name="notification">Уведомление для добавления.</param>
    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет существующее уведомление.
    /// </summary>
    /// <param name="notification">Уведомление для обновления.</param>
    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет уведомление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор уведомления для удаления.</param>
    public async Task DeleteAsync(Guid id)
    {
        var notification = await GetByIdAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Получает все уведомления для пользователя по его идентификатору.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <returns>Список уведомлений пользователя.</returns>
    public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Получает все просроченные книги.
    /// </summary>
    /// <returns>Список просроченных книг.</returns>
    public async Task<IEnumerable<Book>> GetOverdueBooksAsync()
    {
        return await _context.Books
            .Where(b => b.BorrowedAt <= DateTime.UtcNow.AddDays(14) && !b.IsAvailable)
            .ToListAsync();
    }
}