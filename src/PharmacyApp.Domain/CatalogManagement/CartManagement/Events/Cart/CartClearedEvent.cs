using MediatR;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cart
{
    public class CartClearedEvent : DomainEvent , INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public DateTime ClearedAt { get; }

        public CartClearedEvent(Guid cartId, Guid customerId, DateTime clearedAt)
        {
            CartId = cartId;
            CustomerId = customerId;
            ClearedAt = clearedAt;
        }
    }
}