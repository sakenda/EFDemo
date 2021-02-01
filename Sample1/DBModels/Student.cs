using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        //[ConcurrencyCheck]
        public string LastName { get; set; }
        public string Age { get; set; }
        public Address Address { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        public Student()
        {
            Address = new Address();
        }
    }
}