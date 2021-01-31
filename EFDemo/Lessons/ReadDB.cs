using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public static class ReadDB
    {
        // Filter mit Query
        public static void FilterQueryMethod(NorthwindEntities context)
        {
            var result = context.Categories
                                .Where(cat => cat.CategoryName == "Condiments")
                                .SingleOrDefault();
            var prod = context.Entry(result).Collection(p => p.Products).Query();
            var query = prod.Where(p => p.UnitPrice < 20);
            foreach (var item in query)
                Console.WriteLine($"{item.ProductName,-40}{item.UnitPrice}");
        }

        // Explizites Load
        public static void ExplicitLoadMethod(NorthwindEntities context)
        {
            var result = context.Categories
                                .Where(cat => cat.CategoryName == "Seafood")
                                .FirstOrDefault();
            context.Entry(result).Collection(cat => cat.Products).Load();
            foreach (var item in result.Products)
                Console.WriteLine(item.ProductName);

            var result1 = context.Products
                                 .Where(p => p.ProductID == 1)
                                 .FirstOrDefault();
            context.Entry(result1).Reference(p => p.Category).Load();
            Console.WriteLine($"{result1.ProductName,-40}{result1.Category.CategoryName}");
        }

        // Einbinden von mehreren Tabellen durch Include-Methode
        // Problem: Datentabellen können nur gesamt abgefragt werden.
        public static void EagerLoadMultipleTablesMethod(NorthwindEntities context)
        {
            var result = context.Products
                                .Include("Order_Details.Order.Customer")
                                .Where(p => p.ProductName == "Chang");
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductName);
                foreach (var orderDetails in item.Order_Details)
                {
                    Console.WriteLine($"{orderDetails.Order.OrderID,-10}{orderDetails.Order.Customer.CompanyName}");
                }
            }
        }

        // Einbinden von mehreren Tabellen durch Include-Methode
        public static void EagerLoadMethod(NorthwindEntities context)
        {
            var result = context.Categories
                                .Where(cat => cat.CategoryName == "Condiments" ||
                                              cat.CategoryName == "Seafood")
                                .Include(cat => cat.Products);
            foreach (var catItem in result)
            {
                Console.WriteLine(catItem.CategoryName);
                foreach (var prodItem in catItem.Products)
                    Console.WriteLine($"... {prodItem.ProductName}");
            }
        }

        // Lazy Load - Besser deaktivieren da schwer zu kontrollieren
        // Im Konstruktor der DBContext-Klasse folgendes anweisen zum deaktivieren
        // this.Configuration.LazyLoadingEnabled = false;
        public static void LazyLoadMethod(NorthwindEntities context)
        {
            var result = context.Categories
                                .SingleOrDefault(cat => cat.CategoryName == "Condiments");
            if (result != null)
                foreach (var item in result.Products)
                    Console.WriteLine(item.ProductName);
        }

        // Events der Local-Methode
        public static void EventsLoadMethod(NorthwindEntities context)
        {
            context.Products.Local.CollectionChanged += (sender, e) =>
            {
                foreach (Product item in e.NewItems)
                    Console.WriteLine($"Neu: {item.ProductName}");
            };
            context.Products.Add(new Product { ProductName = "Chilli" });
        }

        // Mehrfacher Aufruf von Load
        public static void MultipleLoadMethod(NorthwindEntities context)
        {
            var query = context.Products.Where(p => p.UnitPrice > 100);
            query.Load();
            var query1 = context.Products.Where(p => p.UnitPrice < 10);
            query1.Load();
            foreach (var item in context.Products.Local)
                Console.WriteLine($"{item.ProductName,-40}{item.UnitPrice}");
        }

        // Load- und Local-Methode Alternative
        public static void LoadLocalAltMethod(NorthwindEntities context)
        {
            Console.WriteLine();
            context.Products
                   .Where(p => p.UnitsInStock < 10)
                   .OrderBy(p => p.ProductName).Load();
            foreach (var item in context.Products.Local)
                Console.WriteLine(item.ProductName);
        }

        //  Load- und Local-Methode
        public static void LoadLocalMethod(NorthwindEntities context)
        {
            context.Products.Load();
            var query = context.Products.Local
                                        .Where(p => p.UnitsInStock < 10)
                                        .OrderBy(p => p.ProductName);
            foreach (var item in query)
                Console.WriteLine(item.ProductName);
        }

        // Find-Methode
        public static void FindMethod(NorthwindEntities context)
        {
            var query = context.Products;
            foreach (var item in query)
                Console.WriteLine(item.ProductName);
            Console.WriteLine("\n\nGesuchte ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = query.Find(id);
            if (result == null)
                Console.WriteLine("Produkt nicht gefunden.");
            else
                Console.WriteLine(result.ProductName);
        }
    }
}