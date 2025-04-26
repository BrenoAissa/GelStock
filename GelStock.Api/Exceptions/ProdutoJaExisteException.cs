namespace GelStock.Api.Exceptions
{
    public class ProdutoJaExisteException : GelStockException
    {
        public ProdutoJaExisteException(string nome) : base($"Já existe um produto cadastrado com o nome '{nome}'.") { }
    }
}
