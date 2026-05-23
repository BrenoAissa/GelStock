namespace GelStock.Api.Exceptions
{
    public class UsuarioJaExisteException : Exception
    {
        public UsuarioJaExisteException(string nome, string sobrenome) : base($"O usuário com nome '{nome}' e sobrenome '{sobrenome}' já existe.") { }
    }
}