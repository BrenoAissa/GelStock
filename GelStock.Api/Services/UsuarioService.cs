using GelStock.Api.Data;
using GelStock.Api.Exceptions;
using GelStock.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GelStock.Api.Services
{
    public class UsuarioService
    {
        private readonly GelStockDbContext _gelStockDbContext;
        public UsuarioService(GelStockDbContext gelStockDbContext) { _gelStockDbContext = gelStockDbContext; }

        public async Task<Usuario> CriarUsuarioAsync(Usuario usuario)
        {
            var usuarioComNomeExistente = await _gelStockDbContext.Usuarios.FirstOrDefaultAsync(u => u.Nome == usuario.Nome);
            var usuarioComSobrenomeExistente = await _gelStockDbContext.Usuarios.FirstOrDefaultAsync(u => u.Sobrenome == usuario.Sobrenome);

            if (usuarioComNomeExistente != null || usuarioComSobrenomeExistente != null)
            {
                throw new UsuarioJaExisteException(usuarioComNomeExistente.Nome, usuarioComSobrenomeExistente.Sobrenome);
            }

            _gelStockDbContext.Usuarios.Add(usuario);
            await _gelStockDbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<List<Usuario>> ListarTodosUsuariosAsync()
        {
            return await _gelStockDbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int id)
        {
            var usuario = await _gelStockDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                throw new UsuarioNaoExisteException(id);
            }
            return usuario;
        }

        public async Task<Usuario> AtualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioExistente = await _gelStockDbContext.Usuarios.FindAsync(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new UsuarioNaoExisteException(usuario.Id);
            }
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Sobrenome = usuario.Sobrenome;
            await _gelStockDbContext.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task<Usuario> ExcluirUsuarioAsync(int id)
        {
            var usuario = await _gelStockDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                throw new UsuarioNaoExisteException(id);
            }
            _gelStockDbContext.Usuarios.Remove(usuario);
            await _gelStockDbContext.SaveChangesAsync();
            return usuario;
        }
    }
}
