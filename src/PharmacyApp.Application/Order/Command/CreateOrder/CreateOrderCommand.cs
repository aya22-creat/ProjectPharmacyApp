using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Command.CreateOrder
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
