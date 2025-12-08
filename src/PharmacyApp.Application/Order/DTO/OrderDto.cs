using OrderAgg = PharmacyApp.Domain.OrderManagement.OrderAggregate;
using PharmacyApp.Domain.OrderManagement.Entities;
namespace PharmacyApp.Application.Order.DTO

{
    public record OrderDto(
        Guid Id,
        Guid CustomerId,
        string OrderNumber,
        string Status,
        IEnumerable<OrderItemDto> Items,
        decimal SubTotal,
        decimal ShippingCost,
        decimal TotalAmount,
        string Currency,
        string ShippingAddress,
        string BillingAddress,
        string PaymentMethod,
        DateTime CreatedAt,
        DateTime? ConfirmedAt,
        DateTime? ShippedAt,
        DateTime? DeliveredAt,
        DateTime? CancelledAt,
        string? CancellationReason
    )
    {
        // add a constructor that takes in the domain order entity and maps it to the DTO
        public OrderDto(OrderAgg.Order order) : this(
            Id: order.Id,
            CustomerId: order.CustomerId,
            OrderNumber: order.OrderNumber,
            Status: order.State.ToString(),
            Items: order.Items.Select(static i => new OrderItemDto(
                Id: i.Id,
                Name: i.ProductName,
                Quantity: i.Quantity,
                Price: i.Price.Amount,
                Total: i.GetTotal().Amount,
                Note: string.Empty
            )).ToList(),
            SubTotal: order.SubTotal.Amount,
            ShippingCost: order.ShippingCost.Amount,
            TotalAmount: order.TotalAmount.Amount,
            Currency: "EGP",
            ShippingAddress: order.ShippingAddress,
            BillingAddress: order.BillingAddress,
            PaymentMethod: order.PaymentMethod,
            CreatedAt: order.CreatedAt,
            ConfirmedAt: order.ConfirmedAt,
            ShippedAt: order.ShippedAt,
            DeliveredAt: order.DeliveredAt,
            CancelledAt: order.CancelledAt,
            CancellationReason: order.CancellationReason
        )
        { }
    }
}
