using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace EFDemo
{
    public class ValidationConsole
    {
        public static void DataAnnotations(NorthwindEntities context)
        {
            CheckNameLength(context);
            CheckPrice(context);
            CheckUnits(context);
            GetValidationErrors(context);
            ShowAllValidationErrors(context);
        }

        private static void ShowAllValidationErrors(NorthwindEntities context)
        {
            try
            {
                var prod = context.Products.Find(5);
                var newOrder = new Order() { CustomerID = "ALFKI" };
                var newOrderDetail = new Order_Detail() { Order = newOrder, ProductID = prod.ProductID, Quantity = 20 };

                context.Orders.Add(newOrder);
                context.Order_Details.Add(newOrderDetail);
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError error in item.ValidationErrors)
                    {
                        Console.WriteLine("Fehlerursache in '{0}' \nBeschreibung: {1}", error.PropertyName, error.ErrorMessage);
                    }
                }
            }
        }

        private static void GetValidationErrors(NorthwindEntities context)
        {
            var prod1 = context.Products.Find(1);
            var prod2 = context.Products.Find(2);
            var prod3 = context.Products.Find(3);
            var prod4 = new Product { ProductName = "Apfelstrudel mit Sahne" };
            var prod5 = new Product { ProductName = "Wurst" };

            prod1.ProductName = "Aachener Printe";
            prod2.ProductName = "Original Nürnberger Lebkuchen";

            context.Products.Add(prod4);
            context.Products.Add(prod5);

            var result = context.GetValidationErrors();

            foreach (var item in result)
            {
                if (item.IsValid == false)
                    foreach (var error in item.ValidationErrors)
                    {
                        Console.WriteLine(item.Entry.CurrentValues["ProductName"]);
                        Console.WriteLine($"Fehler in {error.PropertyName}: {error.ErrorMessage}");
                    }

                Console.WriteLine();
            }
        }

        private static void CheckUnits(NorthwindEntities context)
        {
            var prod = context.Products.Single(p => p.ProductID == 33);
            prod.Discontinued = true;
            prod.UnitsOnOrder = 25;
            var errors = context.Entry(prod).GetValidationResult();
            foreach (var item in errors.ValidationErrors)
                Console.WriteLine(item.ErrorMessage);
        }

        private static void CheckPrice(NorthwindEntities context)
        {
            var newProd = context.Products.Single(p => p.ProductID == 1);
            newProd.UnitPrice = -20;
            var result = context.Entry(newProd).GetValidationResult();

            SaveProduct(context, result);
        }

        private static void CheckNameLength(NorthwindEntities context)
        {
            var newProd = new Product { ProductName = "GambasGambasGambasGambasGambasGambasGambasGambasGambasGambasGambasGambasGambasGambasGambas" };
            context.Products.Add(newProd);
            var result = context.Entry(newProd).GetValidationResult();

            SaveProduct(context, result);
        }

        private static void SaveProduct(NorthwindEntities context, DbEntityValidationResult result)
        {
            if (result.IsValid)
            {
                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");
            }
            else
            {
                foreach (var item in result.ValidationErrors)
                    Console.WriteLine($"{item.PropertyName}: {item.ErrorMessage}");
            }
        }
    }
}