namespace GelStock.Api.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }

        public int usuarioId { get; set; }
        public Usuario usuario { get; set; }
       
        public int categoriaId { get; set; }
        public Categoria categoria { get; set; }
    }
}
