using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut
{
    public record CheckoutCompletedMessage(
        //Guid OrderId,
        Guid CheckoutId,
        Guid CustomerId,
        decimal TotalAmount,
        DateTime CompletedAt
    );
   
}