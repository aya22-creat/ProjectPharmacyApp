using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.ValueObjects;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events;

    public class CartItemAddedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public Guid ItemId { get; }
        public Guid ProductId { get; }
        public string ProductName { get; }
        public Money Price { get; }
        public int Quantity { get; }
        public CartItemAddedEvent(Guid cartId, Guid customerId, Guid itemId, Guid productId, string productName, Money price, int quantity, string? currency)
        {
            CartId = cartId;
            CustomerId = customerId;
            ItemId = itemId;
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
    }
