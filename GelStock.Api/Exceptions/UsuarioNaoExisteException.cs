namespace GelStock.Api.Exceptions
{
    public class UsuarioNaoExisteException : Exception
    {
        public UsuarioNaoExisteException(int id) : base($"O usuário com ID {id} não existe.") { }
    }
}
