using MediatR;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Application.Order.Command.AddOrderItem
{
    public record AddOrderItemCommand(
        Guid OrderId,
        Guid ProductId,
        string ProductName,
        int Quantity,
        Money Price
    ) : IRequest<OrderDto>;
}