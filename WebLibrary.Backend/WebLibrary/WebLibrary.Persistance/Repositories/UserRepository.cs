using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Interfaces;
using WebLibrary.Persistance;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="UserRepository"/>.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Пользователь с указанным идентификатором.</returns>
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.BorrowedBooks) // Загрузить все книги, которые были взяты пользователем
            .FirstOrDefaultAsync(u => u.UserId == id); // Ищем пользователя по ID
    }

    /// <summary>
    /// Получает всех пользователей.
    /// </summary>
    /// <returns>Список всех пользователей.</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    /// <summary>
    /// Добавляет нового пользователя.
    /// </summary>
    /// <param name="user">Пользователь для добавления.</param>
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет существующего пользователя.
    /// </summary>
    /// <param name="user">Пользователь для обновления.</param>
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Удаляет пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя для удаления.</param>
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Получает пользователя по email.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Пользователь с указанным email.</returns>
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}