using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Validations
{

    //Validação das propriedades de forma persnalizada
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var primeiraLetra = value.ToString()[0].ToString();
            var segundaLetra = value.ToString()[1].ToString();

            if (segundaLetra != segundaLetra.ToUpper())
            {
                return new ValidationResult("Segunda letra tem que ser maiuscula");
            }

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("Primeira letra tem que ser maiuscula");
            }
            return ValidationResult.Success;

        }
    }
}
