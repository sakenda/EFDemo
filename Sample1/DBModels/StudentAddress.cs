using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.DBModels
{
    public class StudentAddress
    {
        [ForeignKey("Student")]
        public int StudentAddressID { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public Student Student { get; set; }
    }
}