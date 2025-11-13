using System;


namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Product
{
    public record ProductCreatedMessage(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int Quantity,
        DateTime CreatedAt
    );
   
}