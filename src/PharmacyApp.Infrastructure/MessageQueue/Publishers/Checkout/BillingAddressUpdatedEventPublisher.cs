using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CheckoutFunctionality.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
    public class BillingAddressUpdatedEventPublisher:INotificationHandler<BillingAddressUpdatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BillingAddressUpdatedEventPublisher> _logger;

        public BillingAddressUpdatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<BillingAddressUpdatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(BillingAddressUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Billing address updated: CheckoutId={CheckoutId}", notification.CheckoutId);
            await Task.CompletedTask;
        }
    }

}
