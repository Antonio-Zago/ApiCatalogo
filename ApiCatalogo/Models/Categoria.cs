using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models
{
    public class Categoria
    {

        public Categoria()
        {
            //Boa pratica inicializar no construtor as propriedades Collection
            Produtos = new Collection<Produto>();
        }

        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome  { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        public ICollection<Produto>? Produtos  { get; set; }
    }
}
