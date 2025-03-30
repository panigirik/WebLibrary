using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;

namespace WebLibrary.Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /*
        // Настройка свойства CreatedAt для сущности Book
        modelBuilder.Entity<Book>()
            .Property(b => b.BorrowedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder.Entity<Book>()
            .Property(b => b.ReturnBy)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); */

        // Настройка отношения между Book и User
        modelBuilder.Entity<Book>()
            .HasOne(b => b.BorrowedBy) // Связь с пользователем
            .WithMany(u => u.BorrowedBooks) // У пользователя будет коллекция BorrowedBooksIds
            .HasForeignKey(b => b.BorrowedById) // Внешний ключ на BorrowedById
            .OnDelete(DeleteBehavior.SetNull); 

        
        modelBuilder.Entity<Book>()
            .Property(b => b.ImageData)
            .HasColumnType("bytea"); // Указываем PostgreSQL-тип
        
        

    }

}