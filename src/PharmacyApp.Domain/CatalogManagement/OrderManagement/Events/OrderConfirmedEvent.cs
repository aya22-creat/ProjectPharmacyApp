using System;
using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;


namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{
     public class OrderConfirmedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Money TotalAmount { get; }

        public OrderConfirmedEvent(Guid orderId, Guid customerId, Money totalAmount)
        {
           
            OrderId = orderId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
        }
    }
}