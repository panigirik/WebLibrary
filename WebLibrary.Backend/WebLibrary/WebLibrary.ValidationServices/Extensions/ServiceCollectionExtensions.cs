using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebLibrary.Application.Interfaces.UseCases;
using WebLibrary.Application.Interfaces.ValidationInterfaces;
using WebLibrary.Application.Requests;
using WebLibrary.BackgroundService.ClamAV;
using WebLibrary.ValidationServices.Services;
using WebLibrary.ValidationServices.UseCases;
using WebLibrary.ValidationServices.ValidateRules;

namespace WebLibrary.ValidationServices.Extensions;

/// <summary>
/// Класс расширений для добавления сервисов в контейнер зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистация основных сервисов приложения в коллекции служб.
    /// </summary>
    /// <param name="services">Коллекция служб <see cref="IServiceCollection"/> для конфигурации зависимостей.</param>
    public static void AddCoreApplicationValidationServices(this IServiceCollection services)
    {

        services.AddScoped<IValidationUseCase, ValidationUseCase>();
        services.AddScoped<IAddBookUseCaseValidation, AddBookUseCaseValidation>();
        services.AddScoped<IUpdateUserInfoUseCase, UpdateUserInfoUseCase>();
        services.AddScoped<IUpdateBookUseCaseValidation, UpdateBookUseCaseValidation>();

        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<AddBookRequest>, BookValidator>();
        services.AddScoped<IValidator<UpdateBookRequest>, UpdateBookValidator>();
        services.AddScoped<IValidator<UpdateUserInfoRequest>, UserValidator>();
        services.AddScoped<IValidator<UpdateUserInfoRequest>, UserValidator>();

        services.AddScoped<ScanFileForMalwareHelper>(); 
    }
}