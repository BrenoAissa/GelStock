namespace GelStock.Api.Exceptions
{
    public class CategoriaNaoExisteException : GelStockException
    {
        public CategoriaNaoExisteException(int id) : base($"Categoria com ID '{id}' não existe.") { }
    }
}
