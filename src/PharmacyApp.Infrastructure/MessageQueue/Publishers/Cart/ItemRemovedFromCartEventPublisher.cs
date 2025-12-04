using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;

//logging only for demo purposes, no actual message is published

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.Cart{



    public class ItemRemovedFromCartEventPublisher : INotificationHandler<CartItemRemovedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ItemRemovedFromCartEventPublisher> _logger;

        public ItemRemovedFromCartEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<ItemRemovedFromCartEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CartItemRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Item removed from cart: CartId={CartId}, ProductId={ProductId}",
                notification.CartId,
                notification.ProductId
            );

            await Task.CompletedTask;
        }
    }
}