using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

//logger
namespace PharmacyApp.Infrastructure.MessageQueue.CartManagement.Publishers
{
    
    public class CartItemQuantityUpdatedEventPublisher : INotificationHandler<CartItemQuantityUpdatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartItemQuantityUpdatedEventPublisher> _logger;

        public CartItemQuantityUpdatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartItemQuantityUpdatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartItemQuantityUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Cart item quantity updated: CartId={CartId}, ProductId={ProductId}",
                notification.CartId,
                notification.ProductId
            );

            await Task.CompletedTask;
        }
    }
}