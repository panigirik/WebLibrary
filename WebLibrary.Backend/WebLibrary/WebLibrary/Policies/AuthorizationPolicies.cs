namespace WebLibrary.Policies;

/// <summary>
/// Класс, содержащий политику авторизации.
/// </summary>
public static class AuthorizationPolicies
{
    /// <summary>
    /// Метод расширения для добавления кастомных политик авторизации в контейнер сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов для конфигурации.</param>
    /// <remarks>
    /// Этот метод добавляет политику "AdminOnly", которая требует наличия роли "Admin" для доступа.
    /// </remarks>
    public static void AddCustomPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Добавление политики для роли "Admin"
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
        });
    }
}
    
