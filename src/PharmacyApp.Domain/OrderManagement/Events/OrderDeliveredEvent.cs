using System;
using PharmacyApp.Common.Common.DomainEvent;



public class OrderDeliveredEvent : DomainEvent
{

    public Guid OrderId { get; }
    public Guid CustomerId { get; }

    public OrderDeliveredEvent(Guid orderId, Guid customerId)
    {

        OrderId = orderId;
        CustomerId = customerId;
    }
}
