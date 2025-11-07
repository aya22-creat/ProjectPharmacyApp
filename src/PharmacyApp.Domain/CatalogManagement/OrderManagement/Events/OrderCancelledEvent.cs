using System;
using PharmacyApp.Common.Common.DomainEvent;


namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Events
{

      public class OrderCancelledEvent : DomainEvent
    {
        
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }


        public OrderCancelledEvent(Guid orderId, Guid customerId,string reason ,DateTime cancelledAt)
        {
          
            OrderId = orderId;
            CustomerId = customerId;

            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
             CancelledAt = cancelledAt;
        }
    }
}
  
