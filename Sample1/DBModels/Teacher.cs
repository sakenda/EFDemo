using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}