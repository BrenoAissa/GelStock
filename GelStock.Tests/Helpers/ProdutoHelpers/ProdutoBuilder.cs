using GelStock.Api.Models;

namespace Helpers.ProdutoHelpers
{
    class ProdutoBuilder
    {
        private Produto _produto = new Produto
        {
            Nome = "Produto Padrão",
            Tipo = "Tipo Padrão",
            Fabricante = "Fabricante Padrão",
            Quantidade = 1,
            Id = 0
        };

        public ProdutoBuilder ComNome(string nome)
        {
            _produto.Nome = nome;
            return this;
        }

        public ProdutoBuilder ComTipo(string tipo)
        {
            _produto.Tipo = tipo;
            return this;
        }

        public ProdutoBuilder ComFabricante(string fabricante)
        {
            _produto.Fabricante = fabricante;
            return this;
        }

        public ProdutoBuilder ComQuantidade(int quantidade)
        {
            _produto.Quantidade = quantidade;
            return this;
        }

        public ProdutoBuilder ComId(int id)
        {
            _produto.Id = id;
            return this;
        }

        public Produto Build()
        {
            return _produto;
        }
    }
}
