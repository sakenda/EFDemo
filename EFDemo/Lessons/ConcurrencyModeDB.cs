using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFDemo
{
    public class ConcurrencyModeDB
    {
        public static void WriteToDBTwoUserExample(NorthwindEntities context)
        {
            var prodNew = new Product { ProductName = "Milch" };
            context.Products.Add(prodNew);
            context.SaveChanges();
            int id = prodNew.ProductID;

            // Nutzer A nimmt änderungen vor
            var prod1 = context.Products.Where(p => p.ProductID == 1).Single().ProductName = "Speck";
            var prod2 = context.Products.Where(p => p.ProductID == 2).Single().ProductName = "Bier";
            var prod3 = context.Products.Where(p => p.ProductID == 3).Single().ProductName = "Wein";
            var prod4 = context.Products.Where(p => p.ProductID == id).Single().ProductName = "Rum";

            // Nutezer B nimmt änderungen vor
            context.Database.ExecuteSqlCommand("UPDATE Products SET ProductName='Senf' WHERE ProductID=1");
            context.Database.ExecuteSqlCommand("UPDATE Products SET UnitPrice=2.79 WHERE ProductID=2");
            context.Database.ExecuteSqlCommand("DELETE FROM Products WHERE ProductID= " + id.ToString());

            // Nutzer A Speichert
            SaveUpdates(context);
        }

        private static void SaveUpdates(NorthwindEntities context)
        {
            try
            {
                context.SaveChanges();
                Console.WriteLine("Datenbank aktualisiert...");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ResolveConflict(ex);
                SaveUpdates(context);
            }
        }

        private static void ResolveConflict(DbUpdateConcurrencyException ex)
        {
            bool notDeleted = true;

            foreach (var entry in ex.Entries)
            {
                // Allgemeine Informationen ausgeben
                Console.WriteLine("\nEin Konflikt ist aufgetreten; {0}", entry.Entity.GetType());
                Console.WriteLine("Aktuelle Werte (Current): ");
                PrintValues(entry.CurrentValues);
                Console.WriteLine("Ursprüngliche Werte (Original): ");
                PrintValues(entry.OriginalValues);
                var databaseValues = entry.GetDatabaseValues();
                Console.WriteLine("\nNeue Werte in der DB (Database): ");
                notDeleted = PrintValues(databaseValues);

                // Entscheidung wie Konflikt gelöst wird
                Console.WriteLine("Was soll mit Ihren Daten passieren?");
                if (notDeleted)
                    Console.WriteLine("[S]peichern, [V]erwerfen, [M]ergen?");
                else
                    Console.WriteLine("[S]peichern, [V]erwerfen?");
                var action = Console.ReadKey().KeyChar.ToString().ToUpper();

                // Steuerung der Konfliktlösung
                switch (action)
                {
                    case "S":
                        if (!notDeleted)
                        {
                            entry.State = EntityState.Added;
                            break;
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                        break;

                    case "V":
                        entry.Reload();
                        break;

                    case "M":
                        var mergeValues = MergeValues(entry.CurrentValues, entry.OriginalValues, databaseValues);
                        entry.OriginalValues.SetValues(databaseValues);
                        entry.CurrentValues.SetValues(mergeValues);
                        break;

                    default:
                        throw new ArgumentException("Ungültige Eingabe.");
                }

                Console.WriteLine();
            }
        }

        private static bool PrintValues(DbPropertyValues values)
        {
            if (values == null)
            {
                Console.WriteLine("Datensatz ist gelöscht.");
                return false;
            }

            foreach (var propertyName in values.PropertyNames)
                Console.WriteLine("...{0, -16}: {1}", propertyName, values[propertyName]);
            return true;
        }

        private static DbPropertyValues MergeValues(DbPropertyValues current, DbPropertyValues original, DbPropertyValues database)
        {
            DbPropertyValues newCurrent = original.Clone();

            foreach (var propertyName in original.PropertyNames)
            {
                if (!object.Equals(current[propertyName], original[propertyName]))
                    newCurrent[propertyName] = current[propertyName];
                else if (!object.Equals(database[propertyName], original[propertyName]))
                    newCurrent[propertyName] = database[propertyName];
            }

            return newCurrent;
        }
    }
}