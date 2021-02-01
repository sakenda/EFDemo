using Sample1.DBModels;
using System;
using System.Collections.Generic;

namespace Sample1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var teacher = new Teacher
            {
                Name = "Hans Fischer",
                Courses = new List<Course>
                {
                    new Course { Title = "VB.NET" },
                    new Course { Title = "EF" },
                    new Course { Title = "C#" }
                }
            };

            using (var context = new SchoolContext())
            {
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }

            Console.WriteLine("Datenbank aktualisiert...");
            Console.ReadKey();
        }
    }
}