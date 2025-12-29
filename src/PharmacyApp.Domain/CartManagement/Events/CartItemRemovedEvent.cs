using PharmacyApp.Common.Common;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events;
    public class CartItemRemovedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public CartItemRemovedEvent(Guid cartId, Guid productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
           Quantity = quantity;
        }
    }

