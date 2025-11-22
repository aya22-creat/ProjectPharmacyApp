using System;
using PharmacyApp.Common.Common.DomainEvent;
using MediatR;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
{    

    public class CheckoutCreatedEvent : DomainEvent, INotification
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public Guid CartId { get; }
        public decimal TotalAmount { get; }
        public DateTime CreatedAt { get; } 

        public CheckoutCreatedEvent(Guid checkoutId, Guid customerId, Guid cartId, decimal totalAmount, DateTime createdAt)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            CartId = cartId;
            TotalAmount = totalAmount;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
