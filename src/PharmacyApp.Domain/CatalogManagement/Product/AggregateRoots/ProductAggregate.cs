using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;
using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        public string ProductName { get; private set; }
        public ProductDescription Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }

         public string Currency { get; private set; } = "EGP";
        public bool IsCosmetic { get; private set; } // true = Cosmetic, false = Medicine
        public bool IsAvailable => StockQuantity > 0;
        public CategoryId CategoryId { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }



        private ProductAggregate() : base()
        {
            ProductName = null!;
            Description = null!;
            Price = null!;
            CategoryId = null!;
        }

        private ProductAggregate(Guid id, string ProductName, ProductDescription description, Money price, int stockQuantity, bool isCosmetic, CategoryId categoryId)
            : base(id)
        {
            ProductName = ProductName;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            IsCosmetic = isCosmetic;
            CategoryId = categoryId;
        }

        public static ProductAggregate Create(
            string ProductName,
            ProductDescription description,
            Money price,
            int stockQuantity,
            bool isCosmetic,
            CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
                throw new ArgumentException("Product name cannot be empty.");
            if (price.Amount < 0)
                throw new ArgumentException("Product price cannot be negative.");
            if (stockQuantity < 0)
                throw new ArgumentException("Product stock cannot be negative.");

            return new ProductAggregate(Guid.NewGuid(), ProductName, description, price, stockQuantity, isCosmetic, categoryId);
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > StockQuantity)
                throw new ArgumentException("Insufficient stock to reduce.");

            StockQuantity += quantity;
        }

        public void UpdatePrice(Money newPrice)
        {
            if (newPrice.Amount < 0)
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
            ProductName = newName;
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
