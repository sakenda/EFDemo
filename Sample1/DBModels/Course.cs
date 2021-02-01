using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.DBModels
{
    public class Course
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("KursNr")]
        public Guid CourseID { get; set; }

        //[Required, MaxLength(120)]
        public string Title { get; set; }

        public int Duration { get; set; }

        //[Column(TypeName = "money")]
        public decimal Price { get; set; }

        //[Timestamp]
        public byte[] RowVersion { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}