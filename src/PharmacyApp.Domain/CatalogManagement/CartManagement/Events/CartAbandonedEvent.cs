using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events
{
    public class CartAbandonedEvent : DomainEvent
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }

        public CartAbandonedEvent(Guid cartId, Guid customerId, decimal totalAmount)
        {
            CartId = cartId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
        }
    }
}
