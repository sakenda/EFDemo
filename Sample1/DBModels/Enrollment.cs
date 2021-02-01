using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.DBModels
{
    public class Enrollment
    {
        public int StudentID { get; set; }
        public int ScheduleID { get; set; }
    }
}