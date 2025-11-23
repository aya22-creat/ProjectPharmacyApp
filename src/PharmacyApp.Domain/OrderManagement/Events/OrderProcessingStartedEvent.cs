using PharmacyApp.Common.Common.DomainEvent;



namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderProcessingStartedEvent : DomainEvent
    {
        public Guid OrderId { get; }

        public OrderProcessingStartedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}