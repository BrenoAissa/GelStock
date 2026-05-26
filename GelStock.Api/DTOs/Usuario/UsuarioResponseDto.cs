using System.ComponentModel.DataAnnotations;

namespace GelStock.Api.DTOs.Usuario
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Parentesco { get; set; }
    }
}
