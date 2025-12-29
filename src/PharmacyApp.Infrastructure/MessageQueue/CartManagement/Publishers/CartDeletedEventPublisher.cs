using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events;

namespace PharmacyApp.Infrastructure.MessageQueue.CartManagement.Publishers
{
    public class CartDeletedEventPublisher : INotificationHandler<CartDeletedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartDeletedEventPublisher> _logger;

        public CartDeletedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartDeletedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CartDeletedEvent: CartId={CartId}", notification.CartId);

            await _publishEndpoint.Publish(notification, cancellationToken);
        }
    }
}
