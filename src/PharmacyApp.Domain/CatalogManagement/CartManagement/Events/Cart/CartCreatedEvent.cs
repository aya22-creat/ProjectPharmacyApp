using MediatR;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cart
{
     public class CartCreatedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public DateTime CreatedAt { get; }

        public CartCreatedEvent(Guid cartId, Guid customerId, DateTime createdAt)
        {
            CartId = cartId;
            CustomerId = customerId;
            CreatedAt = createdAt;
        }
    }
}