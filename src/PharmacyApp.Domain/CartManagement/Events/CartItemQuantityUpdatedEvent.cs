using PharmacyApp.Common.Common;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events;

    public class CartItemQuantityUpdatedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CartItemId { get; }
        public Guid ProductId { get; }
        public int NewQuantity { get; }
        public int OldQuantity { get; }

        public CartItemQuantityUpdatedEvent(Guid cartId, Guid cartItemId, Guid productId, int newQuantity, int oldQuantity)
        {
            CartId = cartId;
            CartItemId = cartItemId;
            ProductId = productId;
            NewQuantity = newQuantity;
            OldQuantity = oldQuantity;
        }
    }
