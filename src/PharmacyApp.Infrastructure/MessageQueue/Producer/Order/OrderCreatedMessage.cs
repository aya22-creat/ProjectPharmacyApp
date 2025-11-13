using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Order
{
    public record OrderCreatedMessage(
        Guid OrderId,
        Guid CustomerId,
        decimal TotalAmount,
        string Currency,
        DateTime CreatedAt
    );
}