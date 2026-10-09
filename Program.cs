using csharp_rest_api.Data;
using csharp_rest_api.Middlewares; // Importante importar a namespace
using csharp_rest_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos Serviços
builder.Services.AddBancoDeDados(builder.Configuration);
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddControllers();

var app = builder.Build();

// 1. O Middleware de erro DEVE vir primeiro para capturar exceções de tudo que rodar depois dele
app.UseMiddleware<TratamentoErrosMiddleware>();

// 2. Inicializa o banco de dados
app.InicializarBancoDeDados();

app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/api/produtos"));
app.MapControllers();

app.Run();