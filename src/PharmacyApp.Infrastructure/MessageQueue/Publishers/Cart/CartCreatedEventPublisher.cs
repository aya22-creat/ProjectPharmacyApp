using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cart;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;


namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart
{
     public class CartCreatedEventPublisher : INotificationHandler<CartCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartCreatedEventPublisher> _logger;

        public CartCreatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartCreatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CartCreatedMessage: CartId={CartId}", notification.CartId);

            var message = new CartCreatedMessage(
                notification.CartId,
                notification.CustomerId,
                notification.CreatedAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}