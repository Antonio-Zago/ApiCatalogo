using ApiCatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    public class Produto : IValidatableObject
    {

        //Com nome da classe + Id o entityFrameworkCore identifica essa propriedade como chave primaria
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80)]
        //[PrimeiraLetraMaiuscula] Validação personalizada
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public int CategoriaId { get; set; }


        [JsonIgnore]
        public Categoria? Categoria { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var primeiraLetra = Nome.ToString()[0].ToString();

        //    List<ValidationResult> validacoes = new List<ValidationResult>();

        //    if (primeiraLetra != primeiraLetra.ToUpper())
        //    {
        //        validacoes.Add(new ValidationResult("Primeira letra tem que ser maiuscula"));
        //    }

        //    return validacoes;
        //}

        //Mesmo código de cima, serve para retornar lista de IEnumerable<>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var primeiraLetra = Nome.ToString()[0].ToString();


            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new ValidationResult("Primeira letra tem que ser maiuscula");
            }

        }
    }
}
