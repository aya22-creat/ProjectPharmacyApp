using PharmacyApp.Common.Common;
using MediatR;
using PharmacyApp.Domain.CartManagement.Entities;
namespace PharmacyApp.Domain.CartManagement.Events;

    public class CartCheckedOutEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }
        public DateTime CheckedOutAt { get; } = DateTime.UtcNow;
        public string Currency { get; }
        public readonly List<CartItem > _items = new();

        public CartCheckedOutEvent(Guid cartId, Guid customerId, decimal totalAmount, string currency, IEnumerable<CartItem> items)
        {
            CartId = cartId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            Currency = currency;
            _items = items.ToList();
        }
    }

