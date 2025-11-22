using System;
using PharmacyApp.Common.Common.DomainEvent; 
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
{    

    public class CheckoutCompletedEvent : DomainEvent, INotification
    {
        //public Guid OrderId { get; }
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public Guid CartId { get; }
        public DateTime CompletedAt { get; }
        public decimal TotalAmount { get; }

        public CheckoutCompletedEvent(Guid checkoutId, Guid customerId, Guid cartId, DateTime completedAt, decimal totalAmount)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            CartId = cartId;
            CompletedAt = completedAt;
            TotalAmount = totalAmount;
        }
    }
}