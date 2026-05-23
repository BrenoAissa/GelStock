using GelStock.Api.Models;

namespace Helpers.UsuarioHelpers
{
    class UsuarioFactory
    {
        public static Usuario CriarValido()
        {
            return new Usuario
            {
                Nome = "João",
                Sobrenome = "Silva",
                Senha = "Senha123",
                Parentesco = "Responsável",
                Endereco = "Rua Padrão, 123",
            };
        }
    }
}