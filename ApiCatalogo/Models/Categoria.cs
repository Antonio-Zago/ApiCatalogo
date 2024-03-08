using System.Collections.ObjectModel;

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

        public string? Nome  { get; set; }

        public string? ImagemUrl { get; set; }

        public ICollection<Produto>? Produtos  { get; set; }
    }
}
