using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

namespace PharmacyApp.Infrastructure.MessageQueue.CartManagement.Publishers
{
    public class ItemAddedToCartEventPublisher : INotificationHandler<CartItemAddedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ItemAddedToCartEventPublisher> _logger;

        public ItemAddedToCartEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<ItemAddedToCartEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartItemAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Publishing ItemAddedToCartMessage: ProductId={ProductId}",
                notification.ProductId
            );

            var message = new ItemAddedToCartMessage(
                notification.CartId,
                notification.ProductId,
                notification.CustomerId,
                notification.ItemId, 
                notification.ProductName,
                notification.Price.Amount,
                notification.Quantity
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }

}