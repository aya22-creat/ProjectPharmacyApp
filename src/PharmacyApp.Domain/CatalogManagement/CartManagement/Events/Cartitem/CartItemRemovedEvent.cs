using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cartitem
{
    public class CartItemRemovedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CartItemId { get; }
        public Guid ProductId { get; }

        public CartItemRemovedEvent(Guid cartId, Guid cartItemId, Guid productId)
        {
            CartId = cartId;
            CartItemId = cartItemId;
            ProductId = productId;
        }
    }
}
