using System;
using PharmacyApp.Common.Common; 
using PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Events;
using System.Data.Common;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.AggregateRoots
{
    public class ProductAggregate : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public Price Price { get; private set; }
        public int Stock { get; private set; }
        public CategoryId CategoryId { get; private set; }

        private ProductAggregate() : base()
        {
            Name = null!;
            Description = null!;
            Price = null!;
            CategoryId = null!;
        }

        private ProductAggregate(Guid id, string name, ProductDescription description, Price price, int stock, CategoryId categoryId)
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public static ProductAggregate Create(
            string name,
            ProductDescription description,
            Price price,
            int stock,
            CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            if (price.Value < 0)
                throw new ArgumentException("Product price cannot be negative.");

            if (stock < 0)
                throw new ArgumentException("Product stock cannot be negative.");

            var product = new ProductAggregate(Guid.NewGuid(), name, description, price, stock, categoryId);
            product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id, product.Name, product.Price.Value));
            return product;
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > Stock)
                throw new ArgumentException("Insufficient stock to reduce.");

            Stock += quantity;
        }

        public void UpdatePrice(Price newPrice)
        {
            if (newPrice.Value < 0)
                throw new ArgumentException("New price cannot be negative.");

            var oldPrice = Price;
            Price = newPrice;

            RaiseDomainEvent(new ProductPriceChangedDomainEvent(Id, oldPrice.Value, newPrice.Value));
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
    }
}
