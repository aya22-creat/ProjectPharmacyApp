using System;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate
{
    public class CategoryAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ProductCount { get; private set; }

        private CategoryAggregate()
        {
            // default (safe) constructor for ORM
            // Initialize properties with default values
            Name = string.Empty;
            Description = string.Empty;
        }

        private CategoryAggregate(Guid id, string name, string description)
            : base(id)
        {
            Name = name;
            Description = description;
            ProductCount = 0;
        }

        public static CategoryAggregate Create(string name, string description)
        {
            return new CategoryAggregate(Guid.NewGuid(), name, description);
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