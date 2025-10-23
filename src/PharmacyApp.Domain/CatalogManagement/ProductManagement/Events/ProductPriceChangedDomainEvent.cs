using System;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.Events
{
    public class ProductPriceChangedDomainEvent : DomainEvent
    {
        public Guid ProductId { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }

        public ProductPriceChangedDomainEvent(Guid productId, decimal oldPrice, decimal newPrice) : base()
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
