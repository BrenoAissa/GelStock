using GelStock.Api.Data;
using GelStock.Api.Exceptions;
using GelStock.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GelStock.Api.Services
{
    public class ProdutoService
    {
        private readonly GelStockDbContext _gelStockDbContext;
        public ProdutoService(GelStockDbContext gelStockDbContext) { _gelStockDbContext = gelStockDbContext; }

        public async Task<Produto> CriarItemAsync(Produto produto)
        {
            var produtoExistente = await _gelStockDbContext.Produtos.FirstOrDefaultAsync(p => p.Nome == produto.Nome);
            if (produtoExistente != null) throw new ProdutoJaExisteException(produto.Nome);

            _gelStockDbContext.Produtos.Add(produto);
            await _gelStockDbContext.SaveChangesAsync();
            return produto;
        }
        public async Task<List<Produto>> ListarTodosItensAsync()
        {
            return await _gelStockDbContext.Produtos.ToListAsync();
        }
        public async Task<Produto> BuscarItemPorIdAsync(int produtoId)
        {
            return await _gelStockDbContext.Produtos.FindAsync(produtoId);
        }

        public async Task<List<Produto>> ListarItemPorNomeAsync(string produtoNome)
        {
            return await _gelStockDbContext.Produtos.Where(p => p.Nome.Contains(produtoNome)).ToListAsync();
        }

        public async Task<Produto> ExcluirItemAsync(int produtoId)
        {
            var produto = await _gelStockDbContext.Produtos.FindAsync(produtoId);
            if (produto == null) throw new ProdutoNaoExisteException(produtoId);

            _gelStockDbContext.Produtos.Remove(produto);
            await _gelStockDbContext.SaveChangesAsync();
            return produto;
        }
    }
}
