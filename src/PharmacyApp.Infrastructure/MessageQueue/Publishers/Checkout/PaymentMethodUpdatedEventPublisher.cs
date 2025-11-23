using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CheckoutFunctionality.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
 public class PaymentMethodUpdatedEventPublisher : INotificationHandler<PaymentMethodUpdatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<PaymentMethodUpdatedEventPublisher> _logger;

        public PaymentMethodUpdatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<PaymentMethodUpdatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(PaymentMethodUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Payment method updated: CheckoutId={CheckoutId}", notification.CheckoutId);
            await Task.CompletedTask;
        }
    }
}