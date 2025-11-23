using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CartManagement.Events.Cart
{
    public class CartAbandonedEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public int ItemsCount { get; }
        public decimal TotalAmount { get; }

        public CartAbandonedEvent(Guid cartId, Guid customerId, int itemsCount, decimal totalAmount)
        {
            CartId = cartId;
            CustomerId = customerId;
            ItemsCount = itemsCount;
            TotalAmount = totalAmount;
        }
    }
}
