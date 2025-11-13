using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Order
{
    public record OrderCancelledMessage(
        Guid OrderId,
        Guid CustomerId,
         string Reason,
        DateTime CancelledAt
    );
}