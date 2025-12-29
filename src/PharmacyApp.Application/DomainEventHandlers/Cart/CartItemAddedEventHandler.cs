using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PharmacyApp.Application.DomainEventHandlers.Cart;


   public sealed class CartItemAddedEventHandler(
    IStockService stockService,
    ILogger<CartItemAddedEventHandler> logger
) : INotificationHandler<CartItemAddedEvent>
{
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<CartItemAddedEventHandler> _logger = logger;

    public Task Handle(CartItemAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Item added to cart. CartId: {CartId}, ProductId: {ProductId}, Quantity: {Qty}",
            notification.CartId, notification.ProductId, notification.Quantity);

       /** await _stockService.ReserveStockAsync(
            notification.ProductId,
            notification.Quantity,
            cancellationToken
        );**/
        return Task.CompletedTask;
        }
    }

