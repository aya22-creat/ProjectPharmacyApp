using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Cart
{
    public record CartAbandonedMessage(
        Guid CartId,
        Guid CustomerId,
        int ItemsCount,
        decimal TotalAmount
    );
}
