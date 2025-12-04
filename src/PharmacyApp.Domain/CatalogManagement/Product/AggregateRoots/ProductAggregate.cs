using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public Price Price { get; private set; }
        public int StockQuantity  { get; private set; }
        public bool IsCosmetic { get; private set; } // true = Cosmetic, false = Medicine
        public bool IsAvailable => StockQuantity > 0;
        public CategoryId CategoryId { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }



        private ProductAggregate() : base()
        {
            Name = null!;
            Description = null!;
            Price = null!;
             CategoryId = null!;
            
        }

        private ProductAggregate(Guid id, string name, ProductDescription description, Price price, int stockQuantity , bool isCosmetic, CategoryId categoryId)
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            IsCosmetic = isCosmetic;
            CategoryId = categoryId;
        }

        public static ProductAggregate Create(
            string name,
            ProductDescription description,
            Price price,
            int stockQuantity,
            bool isCosmetic,
            CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");
            if (price.Value < 0)
                throw new ArgumentException("Product price cannot be negative.");
            if (stockQuantity < 0)
                throw new ArgumentException("Product stock cannot be negative.");

            return new ProductAggregate(Guid.NewGuid(), name, description, price, stockQuantity, isCosmetic, categoryId);
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > StockQuantity)
                throw new ArgumentException("Insufficient stock to reduce.");

            StockQuantity += quantity;
        }

        public void UpdatePrice(Price newPrice)
        {
            if (newPrice.Value < 0)
                throw new ArgumentException("New price cannot be negative.");

            Price = newPrice;
        }

        public void UpdateCategory(CategoryId newCategoryId)
        {
            CategoryId = newCategoryId ?? throw new ArgumentException("Category cannot be null.");
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Product name cannot be empty.");
            Name = newName;
        }

        public void UpdateDescription(ProductDescription newDescription)
        {
            Description = newDescription ?? throw new ArgumentException("Description cannot be null.");
        }

        public void SetProductType(bool isCosmetic)
        {
            IsCosmetic = isCosmetic;
        }
    }
}
