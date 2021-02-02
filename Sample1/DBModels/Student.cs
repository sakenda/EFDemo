using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public StudentAddress Address { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<Course> Courses { get; set; }
    }
}