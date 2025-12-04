using PharmacyApp.Common.Common;
using System;

using MediatR;


namespace PharmacyApp.Domain.OrderManagement.Events
{
    public class OrderCheckedOutEvent : DomainEvent, INotification
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CheckedOutAt { get; private set; }

        public OrderCheckedOutEvent(Guid orderId, Guid customerId, decimal totalAmount)
        {
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            CheckedOutAt = DateTime.UtcNow;
        }
    }
}
