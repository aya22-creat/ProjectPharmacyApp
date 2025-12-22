using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Message.Producer
{
    public class CheckoutCompletedMessage
    {
        public Guid CheckoutId { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
