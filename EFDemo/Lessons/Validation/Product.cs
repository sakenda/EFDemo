using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFDemo
{
    /*
       Beide herangehensweisen machen das gleiche, welche Methode gewählt wird ist egal.
    */

    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Discontinued && (UnitsOnOrder > 0))
                yield return new ValidationResult("Auslaufartikel. Keine Bestellung möglich", new[] { "UnitsOnOrder" });
            if ((UnitsOnOrder > 0) && (UnitsInStock > 100))
                yield return new ValidationResult("Lagerbestand ausreichend.", new[] { "UnitsOnOrder" });
        }
    }

    [CustomValidation(typeof(Product), "ProductDiscontinued")]
    [CustomValidation(typeof(Product), "CanOrderForStock")]
    public partial class Product
    {
        public static ValidationResult ProductDiscontinued(Product prod, ValidationContext validationContext)
        {
            if (prod.Discontinued && (prod.UnitsOnOrder > 0))
                return new ValidationResult("Auslaufartikel. Keine Bestellung möglich");
            else
                return ValidationResult.Success;
        }

        public static ValidationResult CanOrderForStock(Product prod, ValidationContext validationContext)
        {
            if ((prod.UnitsOnOrder > 0) && (prod.UnitsInStock > 100))
                return new ValidationResult("Lagerbestand ausreichend.");
            else
                return ValidationResult.Success;
        }
    }
}