using System;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CheckoutFunctionality.Events;
using PharmacyApp.Infrastructure.MessageQueue.Producer.CheckOut;

namespace PharmacyApp.Infrastructure.MessageQueue.Publishers.CheckOut
{
     public class ShippingAddressUpdatedEventPublisher : INotificationHandler<ShippingAddressUpdatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ShippingAddressUpdatedEventPublisher> _logger;

        public ShippingAddressUpdatedEventPublisher(
            IPublishEndpoint publishEndpoint,
            ILogger<ShippingAddressUpdatedEventPublisher> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task Handle(ShippingAddressUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Shipping address updated: CheckoutId={CheckoutId}", notification.CheckoutId);
            await Task.CompletedTask;
        }
    }
}