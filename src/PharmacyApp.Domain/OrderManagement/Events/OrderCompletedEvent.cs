using System;
using PharmacyApp.Common.Common;
using MediatR;
using PharmacyApp.Domain.OrderManagement.Entities;


namespace PharmacyApp.Domain.OrderManagement.Events
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