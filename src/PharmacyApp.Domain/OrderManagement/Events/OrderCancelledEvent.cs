using PharmacyApp.Common.Common;
using MediatR;

namespace PharmacyApp.Domain.OrderManagement.Events
{

    public class OrderCancelledEvent : DomainEvent , INotification
    {

        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }


        public OrderCancelledEvent(Guid orderId, Guid customerId, string reason, DateTime cancelledAt)
        {

            OrderId = orderId;
            CustomerId = customerId;

            Reason = reason ?? throw new ArgumentNullException(nameof(reason));
            CancelledAt = cancelledAt;
        }
    }
}

