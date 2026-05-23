using FluentAssertions;
using GelStock.Api.Exceptions;
using GelStock.Api.Services;
using GelStock.Tests.Helpers;
using Helpers.UsuarioHelpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GelStock.Tests.Services
{
    public class UsuarioServiceTests
    {
        [Fact]
        public async Task CriarUsuarioAsync_UsuarioValido_DeveAdicionarComSucesso()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario = UsuarioFactory.CriarValido();

            // Act
            var resultado = await service.CriarUsuarioAsync(usuario);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("João");
            resultado.Sobrenome.Should().Be("Silva");

            var usuariosNoBanco = await context.Usuarios.ToListAsync();
            usuariosNoBanco.Should().ContainSingle();
        }

        [Fact]
        public async Task CriarUsuarioAsync_UsuarioDuplicado_DeveLancarExcecao()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario1 = new UsuarioBuilder()
                .ComNome("João")
                .ComSobrenome("Silva")
                .Build();

            var usuario2 = new UsuarioBuilder()
                .ComNome("João")
                .ComSobrenome("Silva")
                .Build();

            await service.CriarUsuarioAsync(usuario1);

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioJaExisteException>(() => service.CriarUsuarioAsync(usuario2));
        }

        [Fact]
        public async Task ListarTodosUsuariosAsync_DeveRetornarTodosOsUsuarios()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario1 = new UsuarioBuilder()
                .ComNome("João")
                .ComSobrenome("Silva")
                .Build();

            var usuario2 = new UsuarioBuilder()
                .ComNome("Maria")
                .ComSobrenome("Santos")
                .Build();

            await service.CriarUsuarioAsync(usuario1);
            await service.CriarUsuarioAsync(usuario2);

            // Act
            var resultado = await service.ListarTodosUsuariosAsync();

            // Assert
            resultado.Should().NotBeNullOrEmpty();
            resultado.Should().HaveCount(2);
            resultado[0].Nome.Should().Be("João");
            resultado[1].Nome.Should().Be("Maria");
        }

        [Fact]
        public async Task ObterUsuarioPorIdAsync_UsuarioExistente_DeveRetornarUsuario()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario = UsuarioFactory.CriarValido();

            var usuarioCriado = await service.CriarUsuarioAsync(usuario);

            // Act
            var resultado = await service.ObterUsuarioPorIdAsync(usuarioCriado.Id);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(usuarioCriado.Id);
            resultado.Nome.Should().Be("João");
        }

        [Fact]
        public async Task ObterUsuarioPorIdAsync_UsuarioNaoExistente_DeveLancarExcecao()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioNaoExisteException>(() => service.ObterUsuarioPorIdAsync(999));
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_UsuarioValido_DeveAtualizarComSucesso()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario = UsuarioFactory.CriarValido();
            var usuarioCriado = await service.CriarUsuarioAsync(usuario);

            var usuarioAtualizado = new UsuarioBuilder()
                .ComId(usuarioCriado.Id)
                .ComNome("Pedro")
                .ComSobrenome("Oliveira")
                .Build();

            // Act
            var resultado = await service.AtualizarUsuarioAsync(usuarioAtualizado);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Pedro");
            resultado.Sobrenome.Should().Be("Oliveira");

            var usuarioNoBanco = await context.Usuarios.FindAsync(usuarioCriado.Id);
            usuarioNoBanco.Nome.Should().Be("Pedro");
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_UsuarioNaoExistente_DeveLancarExcecao()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario = new UsuarioBuilder()
                .ComId(999)
                .ComNome("João")
                .ComSobrenome("Silva")
                .Build();

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioNaoExisteException>(() => service.AtualizarUsuarioAsync(usuario));
        }

        [Fact]
        public async Task ExcluirUsuarioAsync_UsuarioExistente_DeveExcluirComSucesso()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);
            var usuario = UsuarioFactory.CriarValido();
            var usuarioCriado = await service.CriarUsuarioAsync(usuario);

            // Act
            var resultado = await service.ExcluirUsuarioAsync(usuarioCriado.Id);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(usuarioCriado.Id);

            var usuariosNoBanco = await context.Usuarios.ToListAsync();
            usuariosNoBanco.Should().BeEmpty();
        }

        [Fact]
        public async Task ExcluirUsuarioAsync_UsuarioNaoExistente_DeveLancarExcecao()
        {
            // Arrange
            await using var context = DbContextFactory.Create();
            var service = new UsuarioService(context);

            // Act & Assert
            await Assert.ThrowsAsync<UsuarioNaoExisteException>(() => service.ExcluirUsuarioAsync(999));
        }
    }
}