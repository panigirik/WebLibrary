using WebLibrary.Application.ExceptionsHandling;
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

        builder.WebHost.UseUrls("http://+:5228");

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

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseRouting();
        
        app.UseCors("AllowAllOrigins");
        app.MapControllers();
        app.UseAuthorization();

        // Ensure Swagger is always enabled
        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
    }
}

