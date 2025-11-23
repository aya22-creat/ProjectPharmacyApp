using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CartManagement.ValueObjects;
using PharmacyApp.Domain.CartManagement.Enums;
using PharmacyApp.Domain.CartManagement.Events.Coupon;
using PharmacyApp.Domain.CatalogManagement.Common;

namespace PharmacyApp.Domain.CartManagement.Entities
{
    public class CartItem : BaseEntity<Guid>
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public Money? Price { get; private set; }
        public Money? Discount { get; private set; } = Money.Zero("EGP");
        public Guid CartId { get; private set; }

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

        public CartItem(Guid productId, string productName, int quantity, Money price) : base(Guid.NewGuid())
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty", nameof(productId));

            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("ProductName cannot be empty", nameof(productName));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public void ApplyDiscount(Money discount)
        {
            DiscountValidator.ValidateCartDiscount(discount, GetSubtotal());
            Discount = discount;
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

        public Money GetTotal()
        {
            var total = GetSubtotal();
            if (Discount != null)
                total = total.Subtract(Discount);

            return total;
        }

        internal void SetCartId(Guid cartId)
        {
            CartId = cartId;
        }
    }
}
