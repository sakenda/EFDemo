using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}