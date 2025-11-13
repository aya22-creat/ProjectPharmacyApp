using PharmacyApp.Common.Common.DomainEvent; 

 public class CheckoutCancelledEvent : DomainEvent
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }

        public CheckoutCancelledEvent(Guid checkoutId, Guid customerId, string reason, DateTime cancelledAt)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            Reason = reason;
            CancelledAt = cancelledAt;
        }
    }
