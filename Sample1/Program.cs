using Sample1.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                //context.Database.Log = Console.Write;

                var addr = new Address();
                addr.Country = "D";
                addr.City = "Bonn";
                addr.Street = "Hauptstraße 1";

                context.Students.Add(new Student
                {
                    Address = addr,
                    Age = "23",
                    FirstName = "Peter",
                    LastName = "Schneider"
                });

                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");
            }

            Console.ReadKey();
        }
    }
}