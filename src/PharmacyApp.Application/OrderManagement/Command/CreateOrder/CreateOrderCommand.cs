using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;

namespace PharmacyApp.Application.OrderManagement.Command.CreateOrder
{
    public record CreateOrderCommand(
        Guid CustomerId,
        List<CreateOrderItemDto> Items,
        string Currency,
        string? ShippingAddress,
        string? BillingAddress,
        string? PaymentMethod,
        decimal ShippingCost = 0
    ) : IRequest<OrderDto>;

    public record CreateOrderItemDto(
        Guid ProductId,
        string ProductName,
        int Quantity,
        decimal Price
    );
}
