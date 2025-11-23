using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Messages;
using PharmacyApp.Infrastructure.MessageQueue.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.Consumers.CheckOut
{
     public class CheckoutCompletedConsumer : IConsumer<CheckoutCompletedMessage>
    {
        private readonly ILogger<CheckoutCompletedConsumer> _logger;

        public CheckoutCompletedConsumer(ILogger<CheckoutCompletedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckoutCompletedMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation(
                "Processing CheckoutCompleted: CheckoutId={CheckoutId}, OrderId={OrderId}",
                message.CheckoutId,
                message.OrderId
            );

            await context.Publish(new SendEmailMessage(
                To: "customer@email.com",
                Subject: "Purchase Invoice",
                Body: $"Payment for order #{message.OrderId} has been completed.",
                TemplateId: "invoice-template",
                TemplateData: new { message.OrderId, message.TotalAmount }
            ));


            await Task.CompletedTask;
        }
    }
}