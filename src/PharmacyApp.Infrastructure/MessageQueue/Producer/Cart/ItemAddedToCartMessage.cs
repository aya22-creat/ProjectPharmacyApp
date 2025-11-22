using System;

namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Cart
{
    public record ItemAddedToCartMessage(
        Guid CartId,
        Guid ProductId,
        Guid CustomerId,
        Guid ItemId,
        string ProductName,
        decimal Price,
        int Quantity
    );
    
       
}
