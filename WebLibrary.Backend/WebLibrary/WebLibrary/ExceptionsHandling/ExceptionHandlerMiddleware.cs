using System.Net;
using System.Text.Json;
using WebLibrary.Application.Exceptions;

namespace WebLibrary.ExceptionsHandling;

/// <summary>
/// Класс обработки исключений.
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ExceptionHandlerMiddleware"/>.
    /// </summary>
    /// <param name="next">Следующий обработчик запроса.</param>
    /// <param name="logger">Логгер для записи информации об ошибках.</param>
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Вызывает следующий обработчик запроса и обрабатывает возможные исключения.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    public async Task InvokeAsync(HttpContext context) 
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Обрабатывает исключение и формирует ответ с соответствующим HTTP-статусом.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <param name="exception">Объект исключения.</param>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = exception switch
        {
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            NotFoundException => (int)HttpStatusCode.NotFound,
            ConflictException => (int)HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var errorResponse = new { message = exception.Message };
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
