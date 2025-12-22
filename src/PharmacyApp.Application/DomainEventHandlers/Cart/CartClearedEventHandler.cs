using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using MediatR;
using Microsoft.Extensions.Logging;


namespace PharmacyApp.Application.CartManagements.EventHandlers;


public sealed class CartClearedEventHandler(
    IStockService stockService,
    ILogger<CartClearedEventHandler> logger
) : INotificationHandler<CartClearedEvent>
{
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<CartClearedEventHandler> _logger = logger;

    public async Task Handle(CartClearedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cart cleared. CartId: {CartId}", notification.CartId);

        foreach (var item in notification._items)
        {
            await _stockService.ReleaseStockAsync(
                item.ProductId,
                item.Quantity,
                cancellationToken
            );
        }
    }
}