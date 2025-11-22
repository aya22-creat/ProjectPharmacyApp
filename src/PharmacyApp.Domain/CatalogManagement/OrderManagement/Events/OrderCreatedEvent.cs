using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderCreatedEvent : DomainEvent, INotification
    {

        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public string OrderNumber { get; }
        public decimal TotalAmount { get; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, string orderNumber, decimal totalAmount)
        {

            OrderId = orderId;
            CustomerId = customerId;
            OrderNumber = orderNumber;
            TotalAmount = totalAmount;
        }











    }
}
