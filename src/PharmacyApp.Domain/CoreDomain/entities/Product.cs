using System;
using System.Collections.Generic;
using PharmacyApp.Common.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.CoreDomain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }


        public Product(Guid id, string name, string description, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw DomainException.ForRuleViolation("ProductNameRequired", "Product name cannot be empty.");

            if (price < 0)
                throw DomainException.ForRuleViolation("ProductPriceNonNegative", "Product price cannot be negative.");

            if (stockQuantity < 0)
                throw DomainException.ForRuleViolation("StockQuantityNonNegative", "Stock quantity cannot be negative.");


            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;

        }

       /* public void ChangeCategory(ProductCategory newCategory)
        {
            if (newCategory == null)
                throw DomainException.ForRuleViolation("ProductCategoryRequired", "Product category cannot be null.");

            Category = newCategory;
           // AddDomainEvent(new ProductCategoryChangedEvent(Id, newCategory.Name));
        }
        
        */
        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > StockQuantity)
                throw DomainException.ForRuleViolation("InsufficientStock", "Cannot reduce stock below zero.");

            StockQuantity += quantity;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw DomainException.ForRuleViolation("ProductPriceNonNegative", "Product price cannot be negative.");

            Price = newPrice;
        }
    }
}