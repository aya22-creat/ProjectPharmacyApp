using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.CartManagement.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Entities
{
    public class CartItem : BaseEntity<Guid>
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money? Price { get; private set; }

        // Constructor for EF Core
        private CartItem() : base() { }

        public CartItem(Guid productId, int quantity, Money price) : base(Guid.NewGuid())
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty", nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            ProductId = productId;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(newQuantity));

            Quantity = newQuantity;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));

            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero", nameof(amount));

            if (Quantity - amount < 0)
                throw new ArgumentException("Cannot decrease quantity below zero");

            Quantity -= amount;
        }

        public void UpdatePrice(Money newPrice)
        {
            if (newPrice == null)
                throw new ArgumentNullException(nameof(newPrice));

            Price = newPrice;
        }

        public Money GetSubtotal()
        {
            return Price!.Multiply(Quantity);
        }
    }
}
