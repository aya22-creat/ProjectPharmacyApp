using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
    public class CheckOutCancelledEventPublisher : INotificationHandler<CheckoutCancelledEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CheckOutCancelledEventPublisher> _logger;

        public CheckOutCancelledEventPublisher(IPublishEndpoint publishEndpoint,
            ILogger<CheckOutCancelledEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }
          public async Task Handle(CheckoutCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CheckoutCancelledMessage: CheckoutId={CheckoutId}", notification.CheckoutId);

            var message = new CheckoutCancelledMessage(
                notification.CheckoutId,
                notification.CartId,
                notification.CustomerId,
                notification.Reason,
                notification.CancelledAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }

    }
}