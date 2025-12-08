using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.DomainEventHandlers.Order;

public sealed class OrderCancelledEventHandler(
    IOrderRepository orderRepository,
    IStockService stockService,
    ILogger<OrderCancelledEventHandler> logger) : INotificationHandler<OrderCancelledEvent>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IStockService _stockService = stockService;
    private readonly ILogger<OrderCancelledEventHandler> _logger = logger;

    public async Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling OrderCancelledEvent for OrderId: {OrderId}", notification.OrderId);

        var order = await _orderRepository.GetByIdAsync(notification.OrderId, cancellationToken);
        if (order == null)
        {
            _logger.LogWarning("Order not found: {OrderId}", notification.OrderId);
            return;
        }

        // Restore stock for each order item
        foreach (var item in order.Items)
        {
            try
            {
                await _stockService.ReleaseStockAsync(item.ProductId, item.Quantity, cancellationToken);
                _logger.LogInformation(
                    "Stock released for ProductId: {ProductId}, Quantity: {Quantity}",
                    item.ProductId,
                    item.Quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to release stock for ProductId: {ProductId}",
                    item.ProductId);
            }
        }

        _logger.LogInformation("OrderCancelledEventHandler completed for OrderId: {OrderId}", notification.OrderId);
    }
}
