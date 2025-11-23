using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CheckoutFunctionality.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;


namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
    public class CheckoutCreatedEventPublisher : INotificationHandler<CheckoutCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CheckoutCreatedEventPublisher> _logger;

        public CheckoutCreatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<CheckoutCreatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(CheckoutCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Publishing CheckoutCreatedMessage: CheckoutId={CheckoutId}", notification.CheckoutId);

            var message = new CheckoutCreatedMessage(
                notification.CheckoutId,
                notification.CustomerId,
                notification.CartId,
                notification.TotalAmount,
                notification.CreatedAt
            );

            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }

}