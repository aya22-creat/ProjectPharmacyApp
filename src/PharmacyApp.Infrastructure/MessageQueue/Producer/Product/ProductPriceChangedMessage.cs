using System;


namespace PharmacyApp.Infrastructure.MessageQueue.Producer.Product
{
    public record ProductPriceChangedMessage(
        Guid Id,
        string Name,
        decimal OldPrice,
        decimal NewPrice
        
    );
   
}