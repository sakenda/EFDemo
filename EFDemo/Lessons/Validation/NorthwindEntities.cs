using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EFDemo
{
    public partial class NorthwindEntities
    {
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var errors = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
            var orderDetail = entityEntry.Entity as Order_Detail;

            if (orderDetail != null)
            {
                if (orderDetail.Product.Discontinued == true)
                    errors.ValidationErrors.Add(new DbValidationError("ProductID", orderDetail.Product.ProductName + " ist ein Auslaufartikel"));
            }

            if (errors.ValidationErrors.Count > 0)
                return errors;

            return base.ValidateEntity(entityEntry, items);
        }
    }
}