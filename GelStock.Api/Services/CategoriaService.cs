using GelStock.Api.Data;
using GelStock.Api.Exceptions;
using GelStock.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GelStock.Api.Services
{
    public class CategoriaService
    {
        private readonly GelStockDbContext _gelStockDbContext;
        public CategoriaService(GelStockDbContext gelStockDbContext) { _gelStockDbContext = gelStockDbContext; }

        public async Task<Categoria> CriarCategoriaAsync(Categoria categoria)
        {
            var categoriaExistente = await _gelStockDbContext.Categorias.FirstOrDefaultAsync(c => c.Nome == categoria.Nome);
            if(categoriaExistente != null)
            {
                throw new CategoriaJaExisteException(categoria.Nome);
            }
            _gelStockDbContext.Categorias.Add(categoria);
            await _gelStockDbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task<List<Categoria>> ListarTodasCategoriasAsync()
        {
            return await _gelStockDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> BuscarCategoriaPorIdAsync(int categoriaId)
        {
            var categoria = await _gelStockDbContext.Categorias.FindAsync(categoriaId);
            if (categoria == null)
            {
                throw new CategoriaNaoExisteException(categoriaId);
            }
            return categoria;
        }

        public async Task<Categoria> AtualizarCategoriaAsync(Categoria categoria)
        {
            var categoriaExistente = await _gelStockDbContext.Categorias.FindAsync(categoria.Id);
            if (categoriaExistente == null)
            {
                throw new CategoriaNaoExisteException(categoria.Id);
            }
            categoriaExistente.Nome = categoria.Nome;
            await _gelStockDbContext.SaveChangesAsync();
            return categoriaExistente;
        }

        public async Task<Categoria> ExcluirCategoriaAsync(int categoriaId)
        {
            var categoria = await _gelStockDbContext.Categorias.FindAsync(categoriaId);
            if (categoria == null)
            {
                throw new CategoriaNaoExisteException(categoriaId);
            }
            _gelStockDbContext.Categorias.Remove(categoria);
            await _gelStockDbContext.SaveChangesAsync();
            return categoria;
        }
    }
}
