using Sample1.DBModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sample1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {
                SqlConnection.ClearAllPools();
                //context.Database.Log = Console.Write;

                var student = new Student
                {
                    FirstName = "Willi",
                    LastName = "Krause",
                    Address = new StudentAddress
                    {
                        City = "Bonn",
                        Street = "Hauptstraße 1",
                        Country = "Germany"
                    }
                };

                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");
            }

            Console.ReadKey();
        }
    }
}