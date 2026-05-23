using GelStock.Api.Models;

namespace Helpers.UsuarioHelpers
{
    class UsuarioBuilder
    {
        private Usuario _usuario = new Usuario
        {
            Nome = "João",
            Sobrenome = "Silva",
            Senha = "Senha123",
            Parentesco = "Responsável",
            Endereco = "Rua Padrão, 123",
            Id = 0
        };

        public UsuarioBuilder ComNome(string nome)
        {
            _usuario.Nome = nome;
            return this;
        }

        public UsuarioBuilder ComSobrenome(string sobrenome)
        {
            _usuario.Sobrenome = sobrenome;
            return this;
        }

        public UsuarioBuilder ComSenha(string senha)
        {
            _usuario.Senha = senha;
            return this;
        }

        public UsuarioBuilder ComParentesco(string parentesco)
        {
            _usuario.Parentesco = parentesco;
            return this;
        }

        public UsuarioBuilder ComEndereco(string endereco)
        {
            _usuario.Endereco = endereco;
            return this;
        }

        public UsuarioBuilder ComId(int id)
        {
            _usuario.Id = id;
            return this;
        }

        public Usuario Build()
        {
            return _usuario;
        }
    }
}