namespace WebLibrary.Policies;

public static class AuthorizationPolicies
{
        public static void AddCustomPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy => policy.RequireAuthenticatedUser());
            });
        }
}