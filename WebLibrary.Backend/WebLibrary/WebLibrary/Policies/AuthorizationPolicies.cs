using WebLibrary.Domain.Enums;

namespace WebLibrary.Policies;

public static class AuthorizationPolicies
{
    public static void AddCustomPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin")); 
        });
    }
}


