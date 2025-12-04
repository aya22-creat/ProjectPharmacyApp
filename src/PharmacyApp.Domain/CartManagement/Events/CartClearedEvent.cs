using MediatR;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.Entities;

namespace PharmacyApp.Domain.CartManagement.Events;
    public class CartClearedEvent : DomainEvent , INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public DateTime ClearedAt { get; }
        public readonly List<CartItem > _items = new();

        public CartClearedEvent(Guid cartId, Guid customerId, DateTime clearedAt,IEnumerable<CartItem> items)
        {
            CartId = cartId;
            CustomerId = customerId;
            ClearedAt = clearedAt;
            _items = items.ToList();
        }
    }
