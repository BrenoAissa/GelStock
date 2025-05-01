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
            return await _gelStockDbContext.Produtos.Where(p => produtoNome.Contains(p.Nome)).ToListAsync();
        }

        public async Task<Produto> CriarItemAsync(Produto produto)
        {
            try
            {
                _gelStockDbContext.Produtos.Add(produto);
                await _gelStockDbContext.SaveChangesAsync();
                return produto;
            }
            catch (Exception)
            {
                throw new ProdutoJaExisteException(produto.Nome);
            }
        }

        public async Task<Produto> ExcluirItemAsync(Produto produto)
        {
            try { 
                _gelStockDbContext.Produtos.Remove(produto);
                await _gelStockDbContext.SaveChangesAsync();
                return produto;
            }
            catch (Exception)
            {
                throw new ProdutoNaoExisteException(produto.Nome);
            }
        }
    }
}
