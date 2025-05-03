using GelStock.Api.Data;
using GelStock.Api.Models;
using GelStock.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GelStock.Tests
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _service;
        private readonly GelStockDbContext _context;
        public ProdutoServiceTests() 
        {
            var options = new DbContextOptionsBuilder<GelStockDbContext>()
                .UseInMemoryDatabase(databaseName: "StorageAPP")
                .Options;

            _context = new GelStockDbContext(options);
            _service = new ProdutoService(_context);
        }

        [Fact]
        public async Task CriarItemAsync_ProdutoValido_DeveAdicionarComSucesso()
        {
            // Arrange
            var produto = new Produto { Nome = "Produto Teste", Tipo = "Teste", Fabricante = "Teste", Quantidade = 1};

            // Act
            await _service.CriarItemAsync(produto);

            // Assert
            var produtos = await _context.Produtos.ToListAsync();
            Assert.Single(produtos);
            Assert.Equal("Produto Teste", produtos[0].Nome);
        }

        [Fact]
        public async Task ListarTodosItensAsync_DeveRetornarTodosProdutos()
        {
            //Arrange
            _context.Produtos.Add(new Produto { Nome = "Produto Teste0", Tipo = "Teste0", Fabricante = "Teste0", Quantidade = 1 });
            _context.Produtos.Add(new Produto { Nome = "Produto Teste1", Tipo = "Teste1", Fabricante = "Teste1", Quantidade = 1 });
            await _context.SaveChangesAsync();

            //Act
            var resultado = await _service.ListarTodosItensAsync();

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
        }
    }
}
