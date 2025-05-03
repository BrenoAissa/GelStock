namespace GelStock.Api.Exceptions
{
    public class ProdutoNaoExisteException : GelStockException
    {
        public ProdutoNaoExisteException(int id) : base($"Produto '{id}' não existe") { }
    }
}
