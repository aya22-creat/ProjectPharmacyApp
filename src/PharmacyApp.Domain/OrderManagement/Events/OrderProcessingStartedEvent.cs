using MediatR;
using PharmacyApp.Common.Common;



namespace PharmacyApp.Domain.CatalogManagement.Events
{
    public class OrderProcessingStartedEvent : DomainEvent , INotification
    {
        public Guid OrderId { get; }

        public OrderProcessingStartedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}