using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace csharp_rest_api.Controllers;

public class TokenRequest
{
    public string Client_Id { get; set; } = string.Empty;
    public string Client_Secret { get; set; } = string.Empty;
    public string Grant_Type { get; set; } = string.Empty; // Deve ser "client_credentials"
    public string Scope { get; set; } = string.Empty;
}

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("/token")]
    public IActionResult GerarToken([FromForm] TokenRequest request)
    {
        var clientId = _configuration["AuthSettings:ClientId"];
        var clientSecret = _configuration["AuthSettings:ClientSecret"];

        // Validação das credenciais OAuth 2.0
        if (request.Grant_Type != "client_credentials" ||
            request.Client_Id != clientId ||
            request.Client_Secret != clientSecret)
        {
            throw new BadHttpRequestException("Credenciais OAuth2.0 inválidas ou grant_type não suportado.");
        }

        // Geração do JWT
        var jwtSecret = _configuration["AuthSettings:JwtSecret"]!;
        var key = Encoding.ASCII.GetBytes(jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.Client_Id),
                new Claim("scope", request.Scope ?? "read")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new
        {
            access_token = tokenString,
            token_type = "Bearer",
            expires_in = 3600,
            scope = request.Scope
        });
    }
}