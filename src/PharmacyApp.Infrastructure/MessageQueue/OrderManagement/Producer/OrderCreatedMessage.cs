using System;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer
{
    public record OrderCreatedMessage(
        Guid OrderId,
        Guid CustomerId,
        decimal TotalAmount,
        string Currency,
        DateTime CreatedAt
    );
}