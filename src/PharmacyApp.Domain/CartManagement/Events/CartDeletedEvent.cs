using MediatR;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.Entities;

namespace PharmacyApp.Domain.CartManagement.Events
{
    public class CartDeletedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public DateTime DeletedAt { get; }
            public IReadOnlyList<CartItem> Items { get; }

        public CartDeletedEvent(Guid cartId, Guid customerId, DateTime deletedAt, IReadOnlyList<CartItem> items)
        {
            CartId = cartId;
            CustomerId = customerId;
            DeletedAt = deletedAt;
            Items = items;
        }
    }
}
