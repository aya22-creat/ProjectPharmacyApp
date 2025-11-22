using MassTransit;
using MediatR;
using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Publishers;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Product;
using Microsoft.Extensions.Logging;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Product
{
    public class ProductCreatedEventPublisher : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ProductCreatedEventPublisher> _logger;

        public ProductCreatedEventPublisher(IPublishEndpoint publishEndpoint, ILogger<ProductCreatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing ProductCreatedMessage: ProductId={ProductId}", notification.ProductId);

            var message = new ProductCreatedMessage(
                notification.ProductId,
                notification.Name,
                "SKU-" + notification.ProductId.ToString()[..8],
                notification.Price,
                0,
                DateTime.UtcNow
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
