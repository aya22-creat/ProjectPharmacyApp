using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.Events;

namespace PharmacyApp.Domain.CatalogManagement.CategoryManagement.CategoryAggregate
{
    public class CategoryAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public int DisplayOrder { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int ProductCount { get; private set; }

        private CategoryAggregate()
        {
            // default (safe) constructor for ORM
            // Initialize properties with default values
            Name = string.Empty;
            Description = string.Empty;
        }

        private CategoryAggregate(Guid id, string name, string description, Guid? parentCategoryId, int displayOrder, DateTime createdAt)
            : base(id)
        {
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            DisplayOrder = displayOrder;
            CreatedAt = createdAt;
            ProductCount = 0;
        }

        public static CategoryAggregate Create(string name, string description, Guid? parentCategoryId = null, int displayOrder = 0)
        {
            var category = new CategoryAggregate(Guid.NewGuid(), name, description, parentCategoryId, displayOrder, DateTime.UtcNow);
            category.RaiseDomainEvent(new CategoryCreatedEvent(category.Id, category.Name, category.ParentCategoryId, category.Description, category.DisplayOrder, category.CreatedAt));
            return category;
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
