using PharmacyApp.Common.Common;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events;
    public class CartItemRemovedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CartItemId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; init; }

        public CartItemRemovedEvent(Guid cartId, Guid cartItemId, Guid productId, int quantity)
        {
            CartId = cartId;
            CartItemId = cartItemId;
            ProductId = productId;
           Quantity = quantity;
        }
    }

