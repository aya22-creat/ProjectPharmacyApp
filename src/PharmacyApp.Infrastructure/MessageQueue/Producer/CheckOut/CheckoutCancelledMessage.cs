using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut
{
    public record CheckoutCancelledMessage(

        Guid CheckoutId,
        Guid CartId,
        Guid CustomerId,
       string Reason,
        DateTime CancelledAt

    );
   
}