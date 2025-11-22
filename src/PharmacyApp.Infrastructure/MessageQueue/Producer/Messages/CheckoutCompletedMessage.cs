using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Messages
{
    public class CheckoutCompletedMessage
    {
        public Guid CheckoutId { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
