using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Product;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Messages;

namespace PharmacyApp.Infrastructure.MessageQueue.Consumers.Product
{
    public class ProductOutOfStockConsumer : IConsumer<ProductOutOfStockMessage>
    {
        private readonly ILogger<ProductOutOfStockConsumer> _logger;

        public ProductOutOfStockConsumer(ILogger<ProductOutOfStockConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductOutOfStockMessage> context)
        {
            var message = context.Message;

            _logger.LogWarning(
                "Processing ProductOutOfStock: ProductId={ProductId}, ProductName={ProductName}",
                message.Id,
                message.Name
            );

            await context.Publish(new SendEmailMessage(
                To: "purchasing@pharmacy.com",
                Subject: " Warning: Out of Stock",
                Body: $"Stock depleted for product: {message.Name}"
            ));

            await Task.CompletedTask;
        }
    }
}
