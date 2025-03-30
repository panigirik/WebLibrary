using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebLibrary.Application.Dtos;
using WebLibrary.Application.Interfaces;
using WebLibrary.Domain.Interfaces;

namespace WebLibrary.BackgroundService.Services;

    /// <summary>
    /// Сервис для отправки уведомлений о просроченных книгах.
    /// </summary>
    public class OverdueBookNotificationService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OverdueBookNotificationService> _logger;
        private readonly int _interval;

        /// <summary>
        /// Конструктор для инициализации сервиса.
        /// </summary>
        /// <param name="scopeFactory">Фабрика для создания скоупов.</param>
        /// <param name="logger">Логгер для записей ошибок.</param>
        /// <param name="configuration">Конфигурация для получения интервала уведомлений.</param>
        public OverdueBookNotificationService(IServiceScopeFactory scopeFactory,
            ILogger<OverdueBookNotificationService> logger,
            IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _interval = (int)TimeSpan.FromSeconds(configuration.GetValue<int>("Notifications:TimeSpan")).TotalMilliseconds;
        }

        /// <summary>
        /// Метод для выполнения фоново задачи.
        /// </summary>
        /// <param name="stoppingToken">Токен для отмены задачи.</param>
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

        /// <summary>
        /// Очистка ресурсов.
        /// </summary>
        private void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
