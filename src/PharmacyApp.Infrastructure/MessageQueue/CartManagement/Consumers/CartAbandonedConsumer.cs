using System;
using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Cart;
using PharmacyApp.Infrastructure.MessageQueue.Message.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.Consumers.Cart
{
    public class CartAbandonedConsumer : IConsumer<CartAbandonedMessage>
    {
        private readonly ILogger<CartAbandonedConsumer> _logger;

        public CartAbandonedConsumer(ILogger<CartAbandonedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CartAbandonedMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation(
                "Processing CartAbandoned: CartId={CartId}, TotalAmount={TotalAmount}",
                message.CartId,
                message.TotalAmount
            );

            // Simulate sending an email notification to the customer
            await context.Publish(new SendEmailMessage(
                To: "customer@email.com",
                Subject: "Don't forget your cart!",
                Body: $"You have {message.ItemsCount} items in your cart totaling {message.TotalAmount}. Complete your purchase now!",
                TemplateId: "abandoned-cart-template"
            ));

            await Task.CompletedTask;
        }
    }
}
