using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cartitem
{
    public class CartItemQuantityUpdatedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CartItemId { get; }
        public Guid ProductId { get; }
        public int NewQuantity { get; }

        public CartItemQuantityUpdatedEvent(Guid cartId, Guid cartItemId, Guid productId, int newQuantity)
        {
            CartId = cartId;
            CartItemId = cartItemId;
            ProductId = productId;
            NewQuantity = newQuantity;
        }
    }
}