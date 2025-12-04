using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart
{
    public class CartAbandonedEventPublisher : INotificationHandler<CartAbandonedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartAbandonedEventPublisher> _logger;

        public CartAbandonedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartAbandonedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartAbandonedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CartAbandonedMessage: CartId={CartId}", notification.CartId);

            var message = new CartAbandonedMessage(
                notification.CartId,
                notification.CustomerId,
                notification.ItemsCount,
                notification.TotalAmount
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
