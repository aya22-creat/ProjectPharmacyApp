using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Common.Common;

using PharmacyApp.Domain.CatalogManagement.CategoryManagement;

namespace PharmacyApp.Domain.CoreDomain.Entities
{
    public class Category : BaseEntity<Guid>
    {
    
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public int ProductCount { get; private set; }

        protected Category() { } // defulate as (safe)constructor for ORM

        public Category(Guid id, string name, string description) : base(id)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public static Category Create(string name, string description)
        {
            return new Category(Guid.NewGuid(), name, description);
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

         public void IncrementProductCount()
        {
            ProductCount++;
        }

        public void DecrementProductCount()
        {
            if (ProductCount > 0)
                ProductCount--;
        }
    }
}
