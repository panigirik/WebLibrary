using Microsoft.Extensions.DependencyInjection;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.AuthorInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.BookInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.NotificationInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.RefreshTokenInterfaces;
using WebLibrary.Application.Interfaces.UseCaseIntefaces.UserInterfaces;
using WebLibrary.Application.Mappings;
using WebLibrary.Application.UseCases.AuthorUseCases;
using WebLibrary.Application.UseCases.AuthUseCases;
using WebLibrary.Application.UseCases.BookUseCases;
using WebLibrary.Application.UseCases.NotificationsUseCases;
using WebLibrary.Application.UseCases.RefreshTokenUseCases;
using WebLibrary.Application.UseCases.UserUseCases;



namespace WebLibrary.Application.Extensions;

/// <summary>
/// Класс расширений для добавления сервисов в контейнер зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddCoreApplicationServices(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddAuthorUseCases();
        services.AddAuthUseCases();
        services.AddBookUseCases();
        services.AddNotificationUseCases();
        services.AddRefreshTokenUseCases();
        services.AddUserUseCases();
        

    }
    
    private static void AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMappingProfile));
        services.AddAutoMapper(typeof(AuthorMappingProfile));
        services.AddAutoMapper(typeof(BookMappingProfile));
        services.AddAutoMapper(typeof(NotificationMappingProfile));
        services.AddAutoMapper(typeof(RefreshTokenMappingProfile));
    }

    private static void AddAuthorUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
        services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
        services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
        services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
        services.AddScoped<IGetBooksAuthorUseCase, GetBooksAuthorUseCase>();
        services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
    }

    private static void AddAuthUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<ILogoutUseCase, LogoutUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
    }

    private static void AddBookUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddBookUseCase, AddBookUseCase>();
        services.AddScoped<IBorrowBookUseCase, BorrowBookUseCase>();
        services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
        services.AddScoped<IGetBookByIdUseCase, GetBookByIdUseCase>();
        services.AddScoped<IGetBookByIsbnUseCase, GetBookByIsbnUseCase>();
        services.AddScoped<IGetBookImageUseCase, GetBookImageUseCase>();
        services.AddScoped<IGetBooksByAuthorUseCase, GetBooksByAuthorUseCase>();
        services.AddScoped<IGetPaginatedBooksUseCase, GetPaginatedBooksUseCase>();
        services.AddScoped<IReturnBookUseCase, ReturnBookUseCase>();
        services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
    }


    private static void AddNotificationUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddNotificationUseCase, AddNotificationUseCase>();
        services.AddScoped<IDeleteNotificationUseCase, DeleteNotificationUseCase>();
        services.AddScoped<IGetAllNotificationsUseCase, GetAllNotificationsUseCase>();
        services.AddScoped<IGetNotificationByIdUseCase, GetNotificationByIdUseCase>();
        services.AddScoped<IGetNotificationsByUserIdUseCase, GetNotificationsByUserIdUseCase>();
        services.AddScoped<IMarkNotificationAsReadUseCase, MarkNotificationAsReadUseCase>();
    }

    private static void AddRefreshTokenUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddRefreshTokenUseCase, AddRefreshTokenUseCase>();
        services.AddScoped<IDeleteRefreshTokenUseCase, DeleteRefreshTokenUseCase>();
        services.AddScoped<IGetAllRefreshTokensUseCase, GetAllRefreshTokensUseCase>();
        services.AddScoped<IGetRefreshTokenByIdUseCase, GetRefreshTokenByIdUseCase>();
        services.AddScoped<IGetRefreshTokenByUserIdUseCase, GetRefreshTokenByUserIdUseCase>();
        services.AddScoped<IRevokeRefreshTokenUseCase, RevokeRefreshTokenUseCase>();
    }

    private static void AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddUserUseCase, AddUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
        services.AddScoped<IGetUserByEmailUseCase, GetUserByEmailUseCase>();
        services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
    }
}
