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
     public class CartClearedEventPublisher : INotificationHandler<CartClearedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CartClearedEventPublisher> _logger;

        public CartClearedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CartClearedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartClearedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cart cleared: CartId={CartId}", notification.CartId);
            await Task.CompletedTask;
        }
    }
}
