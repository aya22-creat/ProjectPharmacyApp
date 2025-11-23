using System;
using MediatR;
using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.Events
{
    public class ProductCreatedDomainEvent : DomainEvent , INotification
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public decimal Price { get; }

        public ProductCreatedDomainEvent(Guid productId, string name, decimal price) : base()
        {
            ProductId = productId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
        }
    }
}
