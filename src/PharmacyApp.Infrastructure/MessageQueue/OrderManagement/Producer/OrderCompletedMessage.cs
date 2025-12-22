using System;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer
{
    public record OrderCompletedMessage(
        Guid OrderId,
        Guid CustomerId,
        decimal TotalAmount,
        string Currency,
        DateTime CompletedAt
    );
}