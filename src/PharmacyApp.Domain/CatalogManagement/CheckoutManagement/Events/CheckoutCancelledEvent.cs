using PharmacyApp.Common.Common.DomainEvent; 
using MediatR;
 public class CheckoutCancelledEvent : DomainEvent, INotification
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }

        public Guid CartId { get; set; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }

        public CheckoutCancelledEvent(Guid checkoutId, Guid customerId, Guid cartId, string reason, DateTime cancelledAt)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            CartId = cartId;
            Reason = reason;
            CancelledAt = cancelledAt;
        }
    }
