using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer;
using PharmacyApp.Infrastructure.MessageQueue.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Publishers;

    public class OrderCompletedEventPublisher : INotificationHandler<OrderCompletedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCompletedEventPublisher> _logger;

        public OrderCompletedEventPublisher(IPublishEndpoint publishEndpoint , ILogger<OrderCompletedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OrderCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Publishing OrderCompletedMessage to queue: OrderId={OrderId}",
                notification.OrderId
            );

            var message = new OrderCompletedMessage(
                notification.OrderId,
                notification.CustomerId,
                notification.TotalAmount,
                "EGP",
                notification.CompletedAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);

            _logger.LogInformation("OrderCompletedMessage published successfully");
        }
    }
