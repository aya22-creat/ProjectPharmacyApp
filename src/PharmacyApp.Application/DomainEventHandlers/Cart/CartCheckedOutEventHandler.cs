using PharmacyApp.Domain.CartManagement.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.Carts.EventHandlers;

public sealed class CartCheckedOutEventHandler(
    IOrderRepository orderRepository,
    ILogger<CartCheckedOutEventHandler> logger
) : INotificationHandler<CartCheckedOutEvent>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly ILogger<CartCheckedOutEventHandler> _logger = logger;

    public async Task Handle(CartCheckedOutEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cart checked out. CartId: {CartId}", notification.CartId);

        await _orderRepository.CreateOrderFromCartAsync(
            notification.CartId,
            notification._items,
            notification.CustomerId,
            cancellationToken
        );
    }
}