using csharp_rest_api.Data;
using csharp_rest_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços da aplicação
builder.Services.AddBancoDeDados(builder.Configuration);
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddControllers();

var app = builder.Build();

// Inicializa o banco de dados
app.InicializarBancoDeDados();

app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/api/produtos"));
app.MapControllers();

app.Run();