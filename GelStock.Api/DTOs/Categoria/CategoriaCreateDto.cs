using System.ComponentModel.DataAnnotations;

namespace GelStock.Api.DTOs.Categoria
{
    public class CategoriaCreateDto
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
    }
}