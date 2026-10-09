using Microsoft.AspNetCore.Mvc.Filters;

namespace csharp_rest_api.Filters;

public class ApiKeyFilter : IAsyncActionFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var apiKeyEsperada = _configuration["AuthSettings:ApiKey"];

        if (!context.HttpContext.Request.Headers.TryGetValue("x-api-key", out var apiKeyEnviada) || apiKeyEnviada != apiKeyEsperada)
        {
            throw new BadHttpRequestException("Header 'x-api-key' ausente ou inválido.");
        }

        await next();
    }
}