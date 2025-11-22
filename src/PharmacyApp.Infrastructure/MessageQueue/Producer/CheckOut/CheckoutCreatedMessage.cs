using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut
{
    public record CheckoutCreatedMessage(

        Guid CheckoutId,
        Guid CartId,
        Guid CustomerId,
        decimal TotalAmount,
        DateTime CreatedAt
    );
   
}