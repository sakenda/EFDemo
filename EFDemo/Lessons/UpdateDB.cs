using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public static class UpdateDB
    {
        public static void ChangeEntity(NorthwindEntities context)
        {
            var prod = context.Products
                              .Where(p => p.ProductName == "Chai")
                              .Single();
            prod.ProductName = "Cream Coffee";
            context.SaveChanges();
            Console.WriteLine("Datenbank Aktualisiert");
        }

        public static void AddEntity(NorthwindEntities context)
        {
            var product = new Product { ProductName = "Mustard" };
            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine($"ProductID = {product.ProductID}");
        }

        public static void AddWithNavigationProp(NorthwindEntities context)
        {
            var prod1 = context.Products.Single(prod => prod.ProductID == 5);
            var prod2 = context.Products.Single(prod => prod.ProductID == 6);
            Category newCat = new Category { CategoryName = "Putzmittel" };
            newCat.Products = new List<Product> { prod1, prod2 };
            context.Categories.Add(newCat);

            Console.WriteLine($"Zustand (neue Kategorie): {context.Entry(newCat).State}");
            foreach (var item in newCat.Products)
                Console.WriteLine($"Zustand ({item.ProductName}): {context.Entry(item).State}");
            context.SaveChanges();
            Console.WriteLine("Datenbank aktualisiert...");
        }

        public static void DeleteEntity(NorthwindEntities context)
        {
            try
            {
                // Löschen aus einem Kontext
                var product = context.Products
                                     .Where(p => p.ProductName == "Mustard")
                                     .First();
                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");

                // Löschen ohne Kontext
                var prod = new Product { ProductID = 79 };
                context.Products.Attach(prod);
                context.Products.Remove(prod);
                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kein Datensatz gefunden.");
            }
        }

        public static void DeleteEntityWithRelation(NorthwindEntities context)
        {
            var cat = context.Categories.Single(c => c.CategoryID == 1);
            context.Entry(cat).Collection(c => c.Products).Load();
            context.Categories.Remove(cat);
            context.SaveChanges();
            Console.WriteLine("Datenbank aktualisiert...");
        }
    }
}