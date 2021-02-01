using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}