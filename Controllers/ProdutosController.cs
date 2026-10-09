using Microsoft.AspNetCore.Mvc;
using csharp_rest_api.Filters;
using csharp_rest_api.Models;
using csharp_rest_api.Services;

namespace csharp_rest_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutoService _produtoService;

    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();
        return Ok(produtos);
    }

    [HttpPost]
    [TypeFilter(typeof(ValidacaoProdutoFilter))] // Executa a validação ANTES de entrar no método
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
        var produtoCriado = await _produtoService.AdicionarAsync(produto);
        return CreatedAtAction(nameof(ObterTodos), new { id = produtoCriado.Id }, produtoCriado);
    }
}