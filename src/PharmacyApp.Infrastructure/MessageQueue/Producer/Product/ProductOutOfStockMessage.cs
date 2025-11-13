using System;


namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Product
{
    public record ProductOutOfStockMessage(
        Guid Id,
        string Name,
        decimal Price
    );
   
}