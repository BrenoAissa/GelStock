using System.ComponentModel.DataAnnotations;

namespace GelStock.Api.DTOs.Produto
{
    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
    }
}