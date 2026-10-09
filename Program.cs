using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using csharp_rest_api.Data;
using csharp_rest_api.Middlewares;
using csharp_rest_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBancoDeDados(builder.Configuration);
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddControllers();

// Configuração do JWT Authentication
var jwtSecret = builder.Configuration["AuthSettings:JwtSecret"]!;
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseMiddleware<TratamentoErrosMiddleware>();
app.InicializarBancoDeDados();

// Importante: Habilita a Autenticação e Autorização no Pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();