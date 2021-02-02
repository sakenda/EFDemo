using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.DBModels
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public ICollection<Course> PresenceCourses { get; set; }
        public ICollection<Course> OnlineCourses { get; set; }
    }
}