using System.ComponentModel.DataAnnotations;

namespace Sample1.DBModels
{
    //[ComplexType]
    public class Address
    {
        public int AddressID { get; set; }
        //[Required, MaxLength(40)]
        public string City { get; set; }
        //[Required, MaxLength(50)]
        public string Street { get; set; }
        //[Required, MaxLength(20)]
        public string Country { get; set; }
    }
}