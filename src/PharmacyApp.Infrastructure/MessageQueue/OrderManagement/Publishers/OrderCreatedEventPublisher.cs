
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Publishers;

     public class OrderCreatedEventPublisher : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCreatedEventPublisher> _logger;

        public OrderCreatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<OrderCreatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Publishing OrderCreatedMessage to queue: OrderId={OrderId}",
                notification.OrderId
            );

            var message = new OrderCreatedMessage(
                notification.OrderId,
                notification.CustomerId,
                notification.TotalAmount,
                "EGP",
                System.DateTime.UtcNow
            );

            await _publishEndpoint.Publish(message, cancellationToken);

            _logger.LogInformation("OrderCreatedMessage published successfully");
        }
    }
