using PharmacyApp.Common.Common.DomainEvent;
using MediatR;
namespace PharmacyApp.Domain.CartManagement.Events.Cart
{
    public class CartCheckedOutEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }

        public DateTime CheckedOutAt { get; } = DateTime.UtcNow;
        public string Currency { get; }

        public CartCheckedOutEvent(Guid cartId, Guid customerId, decimal totalAmount, string currency)
        {
            CartId = cartId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            Currency = currency;
        }
    }
}
