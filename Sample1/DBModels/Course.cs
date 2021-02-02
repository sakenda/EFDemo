using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample1.DBModels
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<Student> Students { get; set; }
        public int ReferentID { get; set; }
        public Teacher PresenceTeacher { get; set; }
        public Teacher OnlineTeacher { get; set; }
    }
}