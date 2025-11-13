using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut
{
    public record CheckoutCompletedMessage(

        Guid CheckoutId,
        Guid OrderId,
        Guid CustomerId,
        decimal TotalAmount,
        DateTime CompletedAt
    );
   
}