using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cart
{
    public class CartCheckedOutEvent : DomainEvent
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }
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
