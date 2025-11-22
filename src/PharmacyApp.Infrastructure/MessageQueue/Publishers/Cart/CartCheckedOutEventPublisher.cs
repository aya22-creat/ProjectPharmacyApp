using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Cart;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart
{
    public class CartCheckedOutEventPublisher : INotificationHandler<CartCheckedOutEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartCheckedOutEventPublisher> _logger;

        public CartCheckedOutEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartCheckedOutEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartCheckedOutEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CartCheckedOutMessage: CartId={CartId}", notification.CartId);

            var message = new CartCheckedOutMessage(
                notification.CartId,
                notification.CustomerId,
                notification.CheckedOutAt,
                notification.TotalAmount,
                notification.Currency
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}