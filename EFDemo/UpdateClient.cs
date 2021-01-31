using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFDemo
{
    public class UpdateClient
    {
        public static void ReloadEntries(NorthwindEntities context)
        {
            var prod = context.Products.Where(p => p.ProductID == 1).Single();
            Console.WriteLine($"Vor Reload: {prod.ProductName}");

            context.Database.ExecuteSqlCommand("UPDATE Products SET productName='Kuchen' WHERE ProductID=1");

            context.Entry(prod).Reload();

            Console.WriteLine($"Nach Reload: {prod.ProductName}");
            Console.WriteLine($"Zusatnd: {context.Entry(prod).State}");
        }

        public static void FilterChangedEntities(NorthwindEntities context)
        {
            var prod1 = context.Products.Where(p => p.ProductID == 1).Single();
            var prod2 = context.Products.Where(p => p.ProductID == 2).Single();
            var prod3 = context.Products.Where(p => p.ProductID == 2).Single();
            var cat = context.Categories.Where(c => c.CategoryID == 3).Single();

            prod1.ProductName = "Obstler";
            prod2.ProductName = "Kuchen";
            cat.CategoryName = "Spülmittel";

            foreach (var entry in context.ChangeTracker.Entries<Product>().Where(p => p.State == EntityState.Modified))
            {
                Console.WriteLine($"Aktuell: {entry.CurrentValues["ProductName"]}");
                Console.WriteLine($"Original: {entry.OriginalValues["ProductName"]}\n");
            }

            foreach (var entry in context.ChangeTracker.Entries<Category>().Where(p => p.State == EntityState.Modified))
            {
                Console.WriteLine($"Aktuell: {entry.CurrentValues["CategoryName"]}");
                Console.WriteLine($"Original: {entry.OriginalValues["CategoryName"]}\n");
            }
        }

        public static void ChangeWithCollection(NorthwindEntities context)
        {
            // Kategorie laden
            var cat = context.Categories.Single(c => c.CategoryID == 8);
            var entry = context.Entry(cat);
            // Zugeordnete Produkte laden
            int countBefore = entry.Collection(p => p.Products).CurrentValue.Count;
            Console.WriteLine($"Anzahl der Produkte vorher: {countBefore}");
            // Neu zugeordnetes Produkt laden
            var prodNew = context.Products.Where(p => p.ProductID == 3).Single();
            // Neues Produkt zuordnen
            entry.Collection(c => c.Products).CurrentValue.Add(prodNew);
            int countAfter = entry.Collection(p => p.Products).CurrentValue.Count;
            Console.WriteLine($"Anzahl der Produkte nachher: {countAfter}");
            Console.WriteLine($"Fremdschlüssel vor DetectChanges: {prodNew.CategoryID}");
            context.ChangeTracker.DetectChanges();
            Console.WriteLine($"Fremdschlüssel nach DetectChanges: {prodNew.CategoryID}");
        }

        public static void ChangeWithReference(NorthwindEntities context)
        {
            // Produkt laden
            var prod = context.Products.Single(p => p.ProductID == 11);
            var entry = context.Entry(prod);
            // Zugeordnete Kategorie anzeigen
            string catName = entry.Reference(c => c.Category).CurrentValue.CategoryName;
            Console.WriteLine($"Aktuelle Kategorie: {catName}"); ;
            // Neu zugeordnete Kategorie laden
            var catNew = context.Categories
                                .Where(c => c.CategoryName == "Condiments")
                                .Single();
            // neue Kategorie zuordnen
            entry.Reference(p => p.Category).CurrentValue = catNew;
            Console.WriteLine("Neue Ketegorie: {0}", entry.Reference(c => c.Category).CurrentValue.CategoryName);
            Console.WriteLine($"Zustand: {entry.State}");
        }

        public static void DisplayAllChangedProperties(NorthwindEntities context)
        {
            var prod = context.Products.Single(p => p.ProductID == 1);
            prod.ProductName = "Mehl";
            prod.UnitPrice = 3.99m;
            var entry = context.Entry(prod);
            var propertyNames = entry.CurrentValues.PropertyNames;
            var query = propertyNames.Where(p => entry.Property(p).IsModified);
            foreach (var propertyName in query)
            {
                Console.WriteLine(propertyName);
                Console.WriteLine($"Current: {entry.Property(propertyName).CurrentValue}");
                Console.WriteLine($"Original: {entry.Property(propertyName).OriginalValue}\n");
            }
        }

        public static void ChangeOriginalCurrentValue(NorthwindEntities context)
        {
            var prod = context.Products.Single(p => p.ProductID == 1);
            var entry = context.Entry(prod);
            entry.Property(p => p.ProductName).CurrentValue = "Frikadelle";
            Console.WriteLine($"Current: {entry.Property(p => p.ProductName).CurrentValue}");
            Console.WriteLine($"Original: {entry.Property(p => p.ProductName).OriginalValue}");
            Console.WriteLine($"Zustand: {entry.Property(p => p.ProductName).IsModified}");
        }

        public static void ChangeUndoCurrentOriginalValues(NorthwindEntities context)
        {
            var prod = context.Products.Single(p => p.ProductID == 1);

            Console.WriteLine($"Artikel (original): {prod.ProductName}");
            prod.ProductName = "Schinken";
            Console.WriteLine($"Artikel (current): {prod.ProductName}");
            var entry = context.Entry(prod);

            entry.CurrentValues.SetValues(entry.OriginalValues);
            entry.State = EntityState.Unchanged;

            Console.WriteLine($"Artikel (current): {prod.ProductName}");
            Console.WriteLine($"Zustand: {context.Entry(prod).State}");
        }

        public static void DeletedOrAddedEntities(NorthwindEntities context)
        {
            var result = context.Products.Single(p => p.ProductID == 10);
            result.UnitPrice = (decimal)12.25;
            DbEntityEntry entry = context.Entry(result);
            Console.WriteLine($"ORIGINAL: {entry.OriginalValues["UnitPrice"]}");
            Console.WriteLine($"CURRENT: {entry.CurrentValues["UnitPrice"]}");
            Console.WriteLine($"DATABASE: {entry.GetDatabaseValues()["UnitPrice"]}");

            context.Products.Remove(result);
            var newProd = context.Products.Add(new Product { ProductName = "Kaffee" });
            GetValues(context.Entry(result));
            GetValues(context.Entry(newProd));

            void GetValues(DbEntityEntry e)
            {
                if (e.State == EntityState.Deleted)
                    Console.WriteLine($"Gelöscht: {e.OriginalValues["ProductName"]}");
                if (e.State == EntityState.Added)
                    Console.WriteLine($"Hinzugefügt: {e.CurrentValues["ProductName"]}");
            }
        }

        public static void DBPropertyValuesExample(NorthwindEntities context)
        {
            var prod = context.Products.Single(p => p.ProductID == 1);
            prod.ProductName = "Gemüsebrühe";
            var entry = context.Entry(prod);

            Console.WriteLine($"Typ: {entry.Entity.GetType().ToString()}");
            Console.WriteLine($"Zusatnd: {entry.State.ToString()}");

            Console.WriteLine("\nOriginalValues");
            Console.WriteLine(new string('-', 40));
            foreach (var item in entry.OriginalValues.PropertyNames)
                Console.WriteLine($"{item,-20}: {entry.OriginalValues[item]}");

            Console.WriteLine("\nCurrentValues");
            Console.WriteLine(new string('-', 40));
            foreach (var item in entry.CurrentValues.PropertyNames)
                Console.WriteLine($"{item,-20}: {entry.CurrentValues[item]}");

            Console.WriteLine("\nDatabaseValues");
            Console.WriteLine(new string('-', 40));
            var dbValues = entry.GetDatabaseValues();
            foreach (var item in dbValues.PropertyNames)
                Console.WriteLine($"{item,-20}: {dbValues[item]}");
        }
    }
}