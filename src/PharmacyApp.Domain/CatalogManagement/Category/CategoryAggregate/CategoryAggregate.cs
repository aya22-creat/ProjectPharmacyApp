using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;
using System;

namespace PharmacyApp.Domain.CatalogManagement.Category.CategoryAggregate;

public class CategoryAggregate : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public int DisplayOrder { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int ProductCount { get; private set; }

    private CategoryAggregate()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    private CategoryAggregate(Guid id, string name, string description, int displayOrder, DateTime createdAt)
        : base(id)
    {
        Name = name;
        Description = description;
        DisplayOrder = displayOrder;
        CreatedAt = createdAt;
        ProductCount = 0;
    }

    public static CategoryAggregate Create(string name, string description, int displayOrder = 0)
    {
        return new CategoryAggregate(Guid.NewGuid(), name, description, displayOrder, DateTime.UtcNow);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void IncrementProductCount() => ProductCount++;
    public void DecrementProductCount()
    {
        if (ProductCount > 0)
            ProductCount--;
    }
}
