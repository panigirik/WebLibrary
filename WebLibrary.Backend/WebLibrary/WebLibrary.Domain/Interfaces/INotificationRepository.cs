using WebLibrary.Domain.Entities;

namespace WebLibrary.Domain.Interfaces;

public interface INotificationRepository
{
    Task<Notification> GetByIdAsync(Guid id);
    Task<IEnumerable<Notification>> GetAllAsync();
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(Guid userId);

    
}
