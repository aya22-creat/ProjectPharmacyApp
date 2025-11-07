using System;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderItemAddedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Quantity { get; }

        public OrderItemAddedEvent(Guid orderId, Guid productId, int quantity) : base()
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
