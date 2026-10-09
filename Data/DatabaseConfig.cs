using Microsoft.EntityFrameworkCore;

namespace csharp_rest_api.Data;

public static class DatabaseConfig
{
    public static IServiceCollection AddBancoDeDados(this IServiceCollection services, IConfiguration configuration)
    {
        // Garante que a pasta Data existe
        var pastaData = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        Directory.CreateDirectory(pastaData);

        // Lê a ConnectionString definida no appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? $"Data Source={Path.Combine(pastaData, "banco.db")}";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static void InicializarBancoDeDados(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var contexto = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            contexto.Database.EnsureCreated();
        }
    }
}