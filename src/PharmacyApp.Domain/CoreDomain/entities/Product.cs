using System;
using System.Collections.Generic;
using PharmacyApp.Common.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Domain.CoreDomain.ValueObjects;

namespace PharmacyApp.Domain.CoreDomain.entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public CategoryId CategoryId { get; private set; }

        public Product(Guid id, string name, string description, decimal price, int stock, CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            if (price < 0)
                throw new ArgumentException("Product price cannot be negative.");

            if (stock < 0)
                throw new ArgumentException("Product stock cannot be negative.");

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId ?? throw new ArgumentException("Category cannot be null.");
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && Math.Abs(quantity) > Stock)
                throw new ArgumentException("Insufficient stock to reduce.");

            Stock += quantity;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("New price cannot be negative.");

            Price = newPrice;
        }

        public void UpdateCategory(CategoryId newCategoryId)
        {
            CategoryId = newCategoryId ?? throw new ArgumentException("Category cannot be null.");
        }
    
    }
}
