using System;
using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;


namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderCompletedEvent : DomainEvent
    {
        public Guid OrderId { get; }

        public decimal TotalAmount { get; }
        public DateTime CompletedAt { get; }

        public OrderCompletedEvent(Guid orderId, decimal totalAmount, DateTime completedAt) : base()
        {
            OrderId = orderId;
            TotalAmount = totalAmount;
            CompletedAt = completedAt;
        }
    }
}