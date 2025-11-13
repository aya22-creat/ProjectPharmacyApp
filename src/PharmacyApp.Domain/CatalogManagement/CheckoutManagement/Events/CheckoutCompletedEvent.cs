using System;
using PharmacyApp.Common.Common.DomainEvent; 


namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
{    

    public class CheckoutCompletedEvent : DomainEvent
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public Guid CartId { get; }
        public decimal TotalAmount { get; }

        public CheckoutCompletedEvent(Guid checkoutId, Guid customerId, Guid cartId, decimal totalAmount)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            CartId = cartId;
            TotalAmount = totalAmount;
        }
    }
}