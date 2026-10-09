using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using csharp_rest_api.Models;

namespace csharp_rest_api.Filters;

public class ValidacaoProdutoFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Intercepta a requisição e procura pelo objeto Produto nos parâmetros do método
        var produto = context.ActionArguments.Values.OfType<Produto>().FirstOrDefault();

        if (produto != null)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome) || produto.Preco <= 0)
            {
                context.Result = new BadRequestObjectResult(new 
                { 
                    Mensagem = "Validação recusada no Middleware/Filter: Nome e preço válidos são obrigatórios." 
                });
                return; // Interrompe o fluxo e não deixa chegar ao Controller/Service
            }
        }

        // Se a validação passou, continua a execução para o Controller e Service
        await next();
    }
}