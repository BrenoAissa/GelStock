namespace GelStock.Api.Exceptions
{
    public class GelStockException : Exception
    {
        public GelStockException(string message) : base(message) { }
        public GelStockException(string message, Exception innerException) : base(message, innerException) { }
    }
}
