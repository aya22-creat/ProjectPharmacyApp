using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Messages;
using PharmacyApp.Infrastructure.MessageQueue.Producer;
using PharmacyApp.Infrastructure.MessageQueue.Producer.Product;

public class ProductBackInStockConsumer : IConsumer<ProductBackInStockMessage>
    {
        private readonly ILogger<ProductBackInStockConsumer> _logger;

        public ProductBackInStockConsumer(ILogger<ProductBackInStockConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductBackInStockMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation(
                "Processing ProductBackInStock: ProductId={ProductId}, Quantity={Quantity}",
                message.Id,
                message.Quantity
            );

           

            await Task.CompletedTask;
        }
    }