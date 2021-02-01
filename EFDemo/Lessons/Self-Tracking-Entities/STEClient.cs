using System;
using System.Collections.Generic;

namespace EFDemo
{
    public class STEClient
    {
        public static void ClientConsole(NorthwindEntities context)
        {
            // Produkt abrufen
            IList<Product> liste = STEServer.GetProductsByCategory(2);

            // Preise ändern
            foreach (var item in liste)
            {
                Console.WriteLine($"\n{item.ProductName}");
                Console.WriteLine($"Preis alt: {item.UnitPrice}");
                Console.WriteLine("Preis neu: ");
                item.UnitPrice = Convert.ToDecimal(Console.ReadLine());
                item.State = STEState.Modified;
            }

            // Neues Produkt hinzufügen
            liste.Add(new Product()
            {
                ProductName = "Milch",
                UnitPrice = 0.98m,
                CategoryID = 2,
                State = STEState.Added
            });

            STEServer.SaveProducts(liste);
        }
    }
}