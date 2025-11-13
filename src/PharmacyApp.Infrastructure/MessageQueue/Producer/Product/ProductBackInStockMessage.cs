using System;


namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Product
{
    public record ProductBackInStockMessage(
        Guid Id,
        string Name,
        decimal Price,
        int Quantity

    );
   
}