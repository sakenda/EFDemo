using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDemo
{
    public class STEServer
    {
        public static IList<Product> GetProductsByCategory(int categoryID)
        {
            return new NorthwindEntities().Products
                                          .Where(p => p.CategoryID == categoryID)
                                          .ToList();
        }

        public static void SaveProducts(IEnumerable<Product> products)
        {
            using (var context = new NorthwindEntities())
            {
                foreach (var item in products)
                {
                    if (item.State == STEState.Added)
                    {
                        context.Products.Add(item);
                    }

                    if (item.State == STEState.Deleted)
                    {
                        context.Products.Attach(item);
                        context.Products.Remove(item);
                    }

                    if (item.State == STEState.Modified)
                    {
                        var prod = context.Products
                                          .Where(p => p.ProductID == item.ProductID)
                                          .Single();
                        var entry = context.Entry(prod);
                        entry.CurrentValues.SetValues(item);
                    }

                    Console.WriteLine(context.ChangeTracker.Entries().Count());
                    context.SaveChanges();
                    Console.WriteLine("Datenbank aktualisiert...");
                }
            }
        }
    }
}