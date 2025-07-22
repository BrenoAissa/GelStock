using FluentAssertions;
using GelStock.Api.Services;
using GelStock.Tests.Helpers;
using Helpers.ProdutoHelpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GelStock.Tests.Services
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task CriarItemAsync_ProdutoValido_DeveAdicionarComSucesso()
        {
            // Arrange
            await using var _context = DbContextFactory.Create();
            var _service = new ProdutoService(_context);

            var produto = ProdutoFactory.CriarValido();

            // Act
            await _service.CriarItemAsync(produto);

            // Assert
            var produtos = await _context.Produtos.ToListAsync();
            produtos.Should().ContainSingle();
            produtos[0].Nome.Should().Be("Produto Padrão");
        }
        
        [Fact]
        public async Task ListarTodosItensAsync_DeveRetornarTodosProdutos()
        {
            //Arrange
            await using var _context = DbContextFactory.Create();
            var _service = new ProdutoService(_context);

            var produto1 = ProdutoFactory.CriarValido();
            var produto2 = ProdutoFactory.CriarValido();


            _context.Produtos.Add(produto1);
            _context.Produtos.Add(produto2);

            await _context.SaveChangesAsync();

            //Act
            var resultado = await _service.ListarTodosItensAsync();

            //Assert
            resultado.Should().NotBeNullOrEmpty();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task BuscarItemPorIdAsync_ProdutoExistente_DeveRetornarProduto()
        {
            // Arrange
            await using var _context = DbContextFactory.Create();
            var _service = new ProdutoService(_context);

            var produto = new ProdutoBuilder()
                .ComId(1)
                .Build();

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _service.BuscarItemPorIdAsync(produto.Id);

            // Assert
            resultado.Id.Should().Be(produto.Id);
        }

        [Fact]
        public async Task ExcluirItemPorIdAsync_ProdutoExistente_DeveRemoverProduto()
        {
            // Arrange
            await using var _context = DbContextFactory.Create();
            var _service = new ProdutoService(_context);

            var produto = new ProdutoBuilder()
                .ComId(1)
                .Build();

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            // Act
            var resultado = await _service.ExcluirItemAsync(produto.Id);
            var produtosRestantes = await _context.Produtos.ToListAsync();

            // Assert
            produtosRestantes.Should().BeEmpty();
        }
    }
}
