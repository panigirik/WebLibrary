using WebLibrary.Application.Extensions;
using WebLibrary.BackgroundService.Extensions;
using WebLibrary.Indentity.Extensions;
using WebLibrary.Persistance.Extensions;
using WebLibrary.Policies;
using WebLibrary.ValidationServices.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAppDbContext(builder.Configuration);
        builder.Services.AddCoreApplicationValidationServices();
        builder.Services.AddCustomPolicies();
        builder.Services.AddInfrastructureIdentityServices(builder.Configuration);
        
        builder.Services.AddAuthorization();
        builder.Services.AddCoreApplicationServices();
        builder.Services.AddInfrastructureBackgroundServices(builder.Configuration);
        builder.Services.AddInfrastructureRepositoriesServices();
        
        builder.Services.AddSwaggerAuthentication();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        var app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();

        app.UseRouting();
        
        // Применение политики CORS
        app.UseCors("AllowAllOrigins");
        app.MapControllers();
        app.UseAuthorization();

        // Включение Swagger UI (только в режиме разработки)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); // Включение Swagger
            app.UseSwaggerUI(); // Включение Swagger UI (интерфейс для тестирования API)
        }

        // Запуск приложения
        app.Run();
    }
}