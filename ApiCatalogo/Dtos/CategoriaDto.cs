using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        public string? Nome { get; set; }

        public string? ImagemUrl { get; set; }
    }
}
