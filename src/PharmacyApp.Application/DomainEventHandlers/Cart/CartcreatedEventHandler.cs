using PharmacyApp.Domain.CartManagement.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PharmacyApp.Application.DomainEventHandlers.Cart;

public sealed class CartCreatedEventHandler(
    ILogger<CartCreatedEventHandler> logger
) : INotificationHandler<CartCreatedEvent>
{
    private readonly ILogger<CartCreatedEventHandler> _logger = logger;

    public async Task Handle(CartCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Cart created. CartId: {CartId}, CustomerId: {CustomerId}",
            notification.CartId, notification.CustomerId);
    }
}
