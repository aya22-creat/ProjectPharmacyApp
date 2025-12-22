using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Cart
{
    public record CartCheckedOutMessage(
        Guid CartId,
        Guid CustomerId,
        DateTime CheckedOutAt,
        decimal TotalAmount,
        string Currency

    );
    
      
}
