using PharmacyApp.Domain.CartManagement.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.OrderManagement.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.CartManagements.EventHandlers;

public sealed class CartCheckedOutEventHandler : INotificationHandler<CartCheckedOutEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CartCheckedOutEventHandler> _logger;

    public CartCheckedOutEventHandler(
        IOrderRepository orderRepository,
        ILogger<CartCheckedOutEventHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Handle(CartCheckedOutEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cart checked out. CartId: {CartId}", notification.CartId);

        await _orderRepository.CreateOrderFromCartAsync(
            notification.CartId,
            notification.Items,
            notification.CustomerId,
            cancellationToken
        );
    }
}
