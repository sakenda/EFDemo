using System.ComponentModel.DataAnnotations;

namespace EFDemo
{
    public class ProductValidation
    {
        public static ValidationResult UnitPriceValidation(decimal? price)
        {
            if (price > 0)
                return ValidationResult.Success;

            return new ValidationResult("Preisangabe überprüfen.");
        }
    }
}