using System;
using System.ComponentModel.DataAnnotations;

namespace EFDemo
{
    public class Product_Metadata
    {
        [MaxLength(20, ErrorMessage = "Unzulässige Länge des Produktbezeichners")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Angabe von UnitPrice ist erforderlich")]
        [CustomValidation(typeof(ProductValidation), "UnitPriceValidation")]
        public Nullable<decimal> UnitPrice { get; set; }
    }
}