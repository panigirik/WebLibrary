using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.Persistance.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Notification> GetByIdAsync(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.UserId == id);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await _context.Notifications
            .Include(n => n.User)
            .ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var notification = await GetByIdAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
}