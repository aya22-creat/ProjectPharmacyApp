using System;
using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;


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