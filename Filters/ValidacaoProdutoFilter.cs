using Microsoft.AspNetCore.Mvc.Filters;
using csharp_rest_api.Models;

namespace csharp_rest_api.Filters;

public class ValidacaoProdutoFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var produto = context.ActionArguments.Values.OfType<Produto>().FirstOrDefault();

        if (produto != null)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                erros.Add("O campo 'nome' é obrigatório.");
            }

            if (produto.Preco <= 0)
            {
                erros.Add("O campo 'preco' deve ser maior que zero.");
            }

            if (erros.Count > 0)
            {
                // Junta os erros encontrados em uma única mensagem detalhada
                throw new BadHttpRequestException(string.Join(" ", erros));
            }
        }

        await next();
    }
}