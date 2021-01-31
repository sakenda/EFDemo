using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new NorthwindEntities())
            {
                //context.Database.Log = Console.Write;   // Datenbank abfragen in der Console anzeigen

                //ReadDB.FindMethod(context);
                //ReadDB.LoadLocalMethod(context);
                //ReadDB.LoadLocalAltMethod(context);
                //ReadDB.MultipleLoadMethod(context);
                //ReadDB.EventsLoadMethod(context);
                //ReadDB.LazyLoadMethod(context);
                //ReadDB.EagerLoadMethod(context);
                //ReadDB.EagerLoadMultipleTablesMethod(context);
                //ReadDB.ExplicitLoadMethod(context);
                //ReadDB.FilterQueryMethod(context);

                //UpdateDB.ChangeEntity(context);
                //UpdateDB.AddEntity(context);
                //UpdateDB.AddWithNavigationProp(context);
                //UpdateDB.DeleteEntity(context);
                //UpdateDB.DeleteEntityWithRelation(context);

                //UpdateClient.DBPropertyValuesExample(context);
                //UpdateClient.DeletedOrAddedEntities(context);
                //UpdateClient.ChangeUndoCurrentOriginalValues(context);
                //UpdateClient.ChangeOriginalCurrentValue(context);
                //UpdateClient.DisplayAllChangedProperties(context);
                //UpdateClient.ChangeWithReference(context);
                //UpdateClient.ChangeWithCollection(context);
                //UpdateClient.FilterChangedEntities(context);
                UpdateClient.ReloadEntries(context);
            }

            Console.ReadKey();
        }
    }
}