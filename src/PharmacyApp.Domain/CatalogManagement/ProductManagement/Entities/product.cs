using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.ValueObjects;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.Entities
{
    public class Product : BaseEntity<Guid>
    {
       
        public string Name { get; private set; }
        public ProductDescription Description { get; private set; }
        public Price Price { get; private set; }
        public int Stock { get; private set; }
        public CategoryId CategoryId { get; private set; }

        public Product(Guid id, string name, ProductDescription description, Price price, int stock, CategoryId categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            if (price.Value < 0)
                throw new ArgumentException("Product price cannot be negative.");

            if (stock < 0)
                throw new ArgumentException("Product stock cannot be negative.");

            Id = id;
            Name = name;
            Stock = stock;
            Description = description ?? throw new ArgumentException("Description cannot be null.");
            Price = price ?? throw new ArgumentException("Price cannot be null.");
            CategoryId = categoryId ?? throw new ArgumentException("Category cannot be null.");
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

            Price = newPrice ?? throw new ArgumentException("Price cannot be null.");
        }

        public void UpdateCategory(CategoryId newCategoryId)
        {
            CategoryId = newCategoryId ?? throw new ArgumentException("Category cannot be null.");
        }
    
    }
}
