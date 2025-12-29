using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Producer;

namespace PharmacyApp.Infrastructure.MessageQueue.OrderManagement.Publishers;

    public class OrderCancelledEventPublisher : INotificationHandler<OrderCancelledEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderCancelledEventPublisher> _logger;

        public OrderCancelledEventPublisher(IPublishEndpoint publishEndpoint, ILogger<OrderCancelledEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Publishing OrderCancelledMessage to queue: OrderId={OrderId}",
                notification.OrderId
            );

            var message = new OrderCancelledMessage(
                notification.OrderId,
                notification.CustomerId,
                notification.Reason,
                notification.CancelledAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);

            _logger.LogInformation("OrderCancelledMessage published successfully");
        }
    }

