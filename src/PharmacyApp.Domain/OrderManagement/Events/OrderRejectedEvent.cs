using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.ValueObjects;


namespace PharmacyApp.Domain.OrderManagement.Events;

public class OrderRejectedEvent : DomainEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public DateTime RejectedAt { get; } 
        public String Reason { get; }

        public OrderRejectedEvent(Guid orderId, Guid customerId, DateTime rejectedAt, String reason)
        {
            OrderId = orderId;
            CustomerId = customerId;
            RejectedAt = rejectedAt;
            Reason = reason;
        }
    }
