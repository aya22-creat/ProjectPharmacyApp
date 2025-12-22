using System;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer
{
    public record OrderCancelledMessage(
        Guid OrderId,
        Guid CustomerId,
         string Reason,
        DateTime CancelledAt
    );
}