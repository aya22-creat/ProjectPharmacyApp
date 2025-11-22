using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Cart
{
    public record CartCreatedMessage(
        Guid CartId,
        Guid CustomerId,
        DateTime CreatedAt
    );
}
