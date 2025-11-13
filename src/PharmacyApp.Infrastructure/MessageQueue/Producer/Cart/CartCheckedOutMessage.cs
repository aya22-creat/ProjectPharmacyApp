using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Contracts.Cart
{
    public record CartCheckedOutMessage(
        Guid CartId,
        Guid CustomerId,
        DateTime CheckedOutAt
    );
    
      
}
