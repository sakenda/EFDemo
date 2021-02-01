using System;
using System.Collections.Generic;

namespace Sample1.DBModels
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public DateTime Start { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}