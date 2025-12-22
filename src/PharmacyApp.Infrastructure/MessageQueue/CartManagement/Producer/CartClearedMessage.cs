using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Cart
{
    public record CartClearedMessage(
        Guid CartId,
        Guid CustomerId,
        DateTime ClearedAt
    );
}
