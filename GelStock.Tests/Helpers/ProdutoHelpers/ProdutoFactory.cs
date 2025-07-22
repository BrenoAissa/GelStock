using GelStock.Api.Models;

namespace Helpers.ProdutoHelpers
{
    class ProdutoFactory
    {
        public static Produto CriarValido()
        {
            return new Produto
            {
                Nome = "Produto Padrão",
                Tipo = "Tipo Padrão",
                Fabricante = "Fabricante Padrão",
                Quantidade = 1,
            };
        }
    }
}
