using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
    public class CheckoutCompletedEventPublisher : INotificationHandler<CheckoutCompletedEvent>
    {
    private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CheckoutCompletedEventPublisher> _logger;

        public CheckoutCompletedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CheckoutCompletedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CheckoutCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CheckoutCompletedMessage: CheckoutId={CheckoutId}", notification.CheckoutId);

            var message = new CheckoutCompletedMessage(
                //notification.OrderId,
                notification.CheckoutId,
                notification.CustomerId,
                notification.TotalAmount,
                notification.CompletedAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}