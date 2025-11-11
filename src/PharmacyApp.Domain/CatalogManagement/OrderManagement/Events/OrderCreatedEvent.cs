using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderCreatedEvent : DomainEvent
    {

        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public string OrderNumber { get; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, string orderNumber)
        {

            OrderId = orderId;
            CustomerId = customerId;
            OrderNumber = orderNumber;
        }








        
    }
}