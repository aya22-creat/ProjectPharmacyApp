using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Order
{
    public record OrderCompletedMessage(
        Guid OrderId,
        Guid CustomerId,
        decimal TotalAmount,
        string Currency,
        DateTime CompletedAt
    );
}