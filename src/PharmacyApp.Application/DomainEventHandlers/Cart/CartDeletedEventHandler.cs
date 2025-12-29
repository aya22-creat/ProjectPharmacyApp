using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PharmacyApp.Application.CartManagements.EventHandlers;

public sealed class CartDeletedEventHandler : INotificationHandler<CartDeletedEvent>
{
    private readonly IStockService _stockService;
    private readonly ILogger<CartDeletedEventHandler> _logger;

    public CartDeletedEventHandler(IStockService stockService, ILogger<CartDeletedEventHandler> logger)
    {
        _stockService = stockService;
        _logger = logger;
    }

    public async Task Handle(CartDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Cart deleted. CartId: {CartId}", notification.CartId);

        foreach (var item in notification.Items)
        {
            await _stockService.ReleaseStockAsync(
                item.ProductId,
                item.Quantity,
                cancellationToken
            );
        }
    }
}
