using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.CartManagements.EventHandlers;

public sealed class CartItemQuantityUpdatedEventHandler(
    IStockService stockService,
    ILogger<CartItemQuantityUpdatedEventHandler> logger
) : INotificationHandler<CartItemQuantityUpdatedEvent>
{
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<CartItemQuantityUpdatedEventHandler> _logger = logger;

    public async Task Handle(CartItemQuantityUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Cart item quantity updated. CartId: {CartId}, ProductId: {ProductId}, OldQty: {Old}, NewQty: {New}",
            notification.CartId, notification.ProductId, notification.OldQuantity, notification.NewQuantity);

        int diff = notification.NewQuantity - notification.OldQuantity;

        if (diff > 0)
        {
            await _stockService.ReserveStockAsync(notification.ProductId, diff, cancellationToken);
        }
        else if (diff < 0)
        {
            await _stockService.ReleaseStockAsync(notification.ProductId, -diff, cancellationToken);
        }
    }
}