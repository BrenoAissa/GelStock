namespace GelStock.Api.Exceptions
{
    public class ProdutoNaoExisteException : GelStockException
    {
        public ProdutoNaoExisteException(string nome) : base($"Produto '{nome}' não existe") { }
    }
}
