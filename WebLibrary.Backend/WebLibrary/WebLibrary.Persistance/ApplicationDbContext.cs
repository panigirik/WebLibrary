using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Persistance;

/// <summary>
/// Контекст базы данных приложения, включая все сущности.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Конструктор контекста.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Конфигурирует сущности модели.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasOne(b => b.BorrowedBy)
            .WithMany(u => u.BorrowedBooks) 
            .HasForeignKey(b => b.BorrowedById) 
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Book>()
            .Property(b => b.ImageData)
            .HasColumnType("bytea"); 
    }
}