using System;
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;
using PharmacyApp.Infrastructure.MessageQueue.Message.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.Consumers.Cart
{
    public class ItemAddedToCartConsumer : IConsumer<ItemAddedToCartMessage>
    {
        private readonly ILogger<ItemAddedToCartConsumer> _logger;

        public ItemAddedToCartConsumer(ILogger<ItemAddedToCartConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ItemAddedToCartMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation(
                "Processing ItemAddedToCart: CartId={CartId}, ProductId={ProductId}, Quantity={Quantity}",
                message.CartId,
                message.ItemId,
                message.Quantity
            );

            await context.Publish(new SendPushNotificationMessage(
                UserId: message.CustomerId,
                Title: "Item Added to Cart",
                Message: $"You have added {message.Quantity} of product {message.ProductName} to your cart."
            ));

            await Task.CompletedTask;
        }
    }
}