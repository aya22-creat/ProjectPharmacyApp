using MediatR;
using Microsoft.Extensions.Logging;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Domain.OrderManagement.Repositories;

namespace PharmacyApp.Application.DomainEventHandlers.Order;

public sealed class OrderRejectedEventHandler : INotificationHandler<OrderRejectedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStockService _stockService;
    private readonly ILogger<OrderRejectedEventHandler> _logger;

    public OrderRejectedEventHandler(
        IOrderRepository orderRepository,
        IStockService stockService,
        ILogger<OrderRejectedEventHandler> logger)
    {
        _orderRepository = orderRepository;
        _stockService = stockService;
        _logger = logger;
    }

    public async Task Handle(OrderRejectedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling OrderRejectedEvent for OrderId: {OrderId}", notification.OrderId);

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

        _logger.LogInformation("OrderRejectedEventHandler completed for OrderId: {OrderId}", notification.OrderId);
    }
}
