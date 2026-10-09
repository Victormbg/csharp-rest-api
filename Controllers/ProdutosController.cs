using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using csharp_rest_api.Filters;
using csharp_rest_api.Models;
using csharp_rest_api.Services;

namespace csharp_rest_api.Controllers;

[ApiController]
[Authorize] // Exige Token JWT válido no Header Authorization: Bearer <token>
[TypeFilter(typeof(ApiKeyFilter))] // Exige o Header x-api-key
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
        return Ok(new { status = "sucesso", total = produtos.Count, dados = produtos });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(string id)
    {
        var produto = await _produtoService.ObterPorIdAsync(id);
        return Ok(produto);
    }

    [HttpPost]
    [TypeFilter(typeof(ValidacaoProdutoFilter))]
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
        var produtoCriado = await _produtoService.AdicionarAsync(produto);
        return CreatedAtAction(nameof(ObterPorId), new { id = produtoCriado.Id }, produtoCriado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        await _produtoService.DeletarAsync(id);
        return NoContent();
    }
}