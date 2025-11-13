using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Contracts.Cart
{
    public record CartCreatedMessage(
        Guid CartId,
        Guid CustomerId,
        DateTime CreatedAt
    );
}
