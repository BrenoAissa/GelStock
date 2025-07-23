namespace GelStock.Api.Exceptions
{
    public class CategoriaJaExisteException : GelStockException
    {
        public CategoriaJaExisteException(string nome) : base($"Já existe uma categoria cadastrado com o nome '{nome}'.") { }
    }
}
