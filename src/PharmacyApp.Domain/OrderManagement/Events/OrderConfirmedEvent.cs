using PharmacyApp.Common.Common;
using PharmacyApp.Domain.OrderManagement.ValueObjects;


namespace PharmacyApp.Domain.OrderManagement.Events
{
    public class OrderConfirmedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Money TotalAmount { get; }

        public OrderConfirmedEvent(Guid orderId, Guid customerId, Money totalAmount)
        {

            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
        }
    }
}