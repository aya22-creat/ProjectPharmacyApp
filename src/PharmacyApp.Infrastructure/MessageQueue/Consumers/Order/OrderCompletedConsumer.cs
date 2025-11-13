using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Order;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Messages;
using PharmacyApp.Infrastructure.MessageQueue.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.Consumers.Order
{
    public class OrderCompletedConsumer : IConsumer<OrderCompletedMessage>
    {
        private readonly ILogger<OrderCompletedConsumer> _logger;

       public OrderCompletedConsumer(ILogger<OrderCompletedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCompletedMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation("Processing OrderCompleted: OrderId={OrderId}", message.OrderId);



                await context.Publish(new SendEmailMessage(
                    To: "customer@email.com",
                    Subject: "Your order is complete",
                    Body: $"Your order #{message.OrderId} has been completed successfully"
                ));

                await Task.CompletedTask;
        }
    }
}
