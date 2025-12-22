using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer;
using PharmacyApp.Infrastructure.MessageQueue.Message.Producer;


namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedMessage>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;
        

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
        {
            var message = context.Message;
            _logger.LogInformation("Order Created Message Received: OrderId={OrderId}, UserId={UserId}, CreatedAt={CreatedAt}",
                message.OrderId, message.CustomerId, message.CreatedAt);


             try
            {


                await context.Publish(new SendPushNotificationMessage(
                    message.CustomerId,
                    "Your order has been created",
                    $"Order number: {message.OrderId}"
                ));

                _logger.LogInformation("OrderCreated processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing OrderCreatedMessage");
                throw; 
        }
    }
}
}