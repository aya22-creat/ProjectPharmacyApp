using System;
using PharmacyApp.Common.Common.DomainEvent;
using MediatR;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;


namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
    public class OrderCompletedEvent : DomainEvent , INotification
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }
        public DateTime CompletedAt { get; }

        public OrderCompletedEvent(Guid orderId, Guid customerId, decimal totalAmount, DateTime completedAt) : base()
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            CompletedAt = completedAt;
        }
    }
}