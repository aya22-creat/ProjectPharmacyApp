using System;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderItemRemovedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid OrderItemId { get; }

        public OrderItemRemovedEvent(Guid orderId, Guid orderItemId) : base()
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
        }
    }
}
