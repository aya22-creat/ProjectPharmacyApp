using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;
using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Application.OrderManagement.Command.AddOrderItem
{
    public record AddOrderItemCommand(
        Guid OrderId,
        Guid ProductId,
        string ProductName,
        int Quantity,
        Money Price
    ) : IRequest<OrderDto>;
}