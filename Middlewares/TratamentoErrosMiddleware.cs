using System.Net;
using System.Text.Json;

namespace csharp_rest_api.Middlewares;

public class TratamentoErrosMiddleware
{
    private readonly RequestDelegate _next;

    public TratamentoErrosMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            // Erro 404 (ID não encontrado)
            await TratarErroAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            // Erro 400 (Validação do Filter recusada)
            await TratarErroAsync(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception)
        {
            // Erro 500 (Erro não esperado no servidor)
            await TratarErroAsync(context, HttpStatusCode.InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }

    private static Task TratarErroAsync(HttpContext context, HttpStatusCode status, string mensagem)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var resposta = new
        {
            status = "erro",
            mensagem
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        return context.Response.WriteAsync(JsonSerializer.Serialize(resposta, jsonOptions));
    }
}