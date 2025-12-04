using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Carts.EventHandlers;

public sealed class CartItemRemovedEventHandler(
    IStockService stockService,
    ILogger<CartItemRemovedEventHandler> logger
) : INotificationHandler<CartItemRemovedEvent>
{
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<CartItemRemovedEventHandler> _logger = logger;

    public async Task Handle(CartItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Item removed from cart. CartId: {CartId}, ProductId: {ProductId}, Quantity: {Qty}",
            notification.CartId, notification.ProductId, notification.Quantity);

        await _stockService.ReleaseStockAsync(
            notification.ProductId,
            notification.Quantity,
            cancellationToken
        );
    }
}
