using Microsoft.EntityFrameworkCore;
using csharp_rest_api.Data;
using csharp_rest_api.Models;

namespace csharp_rest_api.Services;

public class ProdutoService
{
    private readonly AppDbContext _contexto;

    public ProdutoService(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Produto>> ObterTodosAsync()
    {
        return await _contexto.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto> ObterPorIdAsync(string stringId)
    {
        if (!Guid.TryParse(stringId, out var guidId))
        {
            throw new KeyNotFoundException("Produto não encontrado");
        }

        var produto = await _contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == guidId);

        if (produto == null)
        {
            throw new KeyNotFoundException("Produto não encontrado");
        }

        return produto;
    }

    public async Task<Produto> AdicionarAsync(Produto produto)
    {
        await _contexto.Produtos.AddAsync(produto);
        await _contexto.SaveChangesAsync();
        return produto;
    }

    public async Task DeletarAsync(string stringId)
    {
        if (!Guid.TryParse(stringId, out var guidId))
        {
            throw new KeyNotFoundException("Produto não encontrado");
        }

        var produto = await _contexto.Produtos.FindAsync(guidId);

        if (produto == null)
        {
            throw new KeyNotFoundException("Produto não encontrado");
        }

        _contexto.Produtos.Remove(produto);
        await _contexto.SaveChangesAsync();
    }
}