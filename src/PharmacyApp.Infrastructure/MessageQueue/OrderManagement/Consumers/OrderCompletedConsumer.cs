using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer;
using PharmacyApp.Infrastructure.MessageQueue.Message.Producer;
using PharmacyApp.Infrastructure.MessageQueue.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Consumers
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
