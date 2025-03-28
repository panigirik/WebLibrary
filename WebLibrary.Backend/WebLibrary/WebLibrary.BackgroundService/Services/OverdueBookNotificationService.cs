using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.BackgroundService.Services;

public class OverdueBookNotificationService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OverdueBookNotificationService> _logger;
    private readonly int _interval;

    public OverdueBookNotificationService(IServiceScopeFactory scopeFactory,
        ILogger<OverdueBookNotificationService> logger,
        IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _interval = (int)TimeSpan.FromSeconds(configuration.GetValue<int>("Notifications:TimeSpan")).TotalMilliseconds;


    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var bookRepository = scope.ServiceProvider.GetRequiredService<IBookRepository>();
                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var overdueBooks = await bookRepository.GetOverdueBooksAsync();

                foreach (var book in overdueBooks)
                {
                    var bookDetails = await bookService.GetBookByIdAsync(book.BookId);
                    if (bookDetails == null)
                        continue;

                    var notification = new NotificationDto
                    {
                        NotificationId = Guid.NewGuid(),
                        UserId = bookDetails.BorrowedById,
                        Message = $"Книга '{bookDetails.Title}' должна быть возвращена {book.BorrowedAt:dd.MM.yyyy}. Пожалуйста, верните её.",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false
                    };

                    await notificationService.AddNotificationAsync(notification);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при отправке уведомлений о просроченных книгах");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private void Dispose()
    {
        Dispose();
        GC.SuppressFinalize(this);
    }
}
