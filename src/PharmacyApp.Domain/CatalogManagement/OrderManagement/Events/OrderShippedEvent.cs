using System;
using PharmacyApp.Common.Common.DomainEvent;


public class OrderShippedEvent : DomainEvent
{

    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public string ShippingAddress { get; }

    public OrderShippedEvent(Guid orderId, Guid customerId, string shippingAddress)
    {

        OrderId = orderId;
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
    }
}
    