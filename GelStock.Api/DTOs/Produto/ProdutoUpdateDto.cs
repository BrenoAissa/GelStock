using System.ComponentModel.DataAnnotations;

namespace GelStock.Api.DTOs.Produto
{
    public class ProdutoUpdateDto
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID deve ser válido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo do produto é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O tipo deve ter entre 2 e 100 caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O fabricante é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O fabricante deve ter entre 2 e 100 caracteres")]
        public string Fabricante { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID da categoria deve ser válido")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do usuário deve ser válido")]
        public int UsuarioId { get; set; }
    }
}