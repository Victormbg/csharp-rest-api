using Microsoft.EntityFrameworkCore;

namespace csharp_rest_api.Data;

public static class DatabaseConfig
{
    public static IServiceCollection AddBancoDeDados(this IServiceCollection services, IConfiguration configuration)
    {
        // Garante que a pasta Data seja criada na raiz do projeto
        var pastaData = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        Directory.CreateDirectory(pastaData);

        // Define o caminho absoluto exato para o banco de dados SQLite
        var caminhoBanco = Path.Combine(pastaData, "banco.db");
        var connectionString = $"Data Source={caminhoBanco}";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }

    public static void InicializarBancoDeDados(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var contexto = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            // Força a criação do arquivo no disco com as tabelas
            contexto.Database.EnsureCreated();
        }
    }
}