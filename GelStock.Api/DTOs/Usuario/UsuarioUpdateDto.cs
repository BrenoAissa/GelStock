using System.ComponentModel.DataAnnotations;

namespace GelStock.Api.DTOs.Usuario
{
    public class UsuarioUpdateDto
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome do usuário é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O sobrenome deve ter entre 3 e 100 caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O parentesco é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O parentesco deve ter entre 2 e 50 caracteres")]
        public string Parentesco { get; set; }

    }
}
