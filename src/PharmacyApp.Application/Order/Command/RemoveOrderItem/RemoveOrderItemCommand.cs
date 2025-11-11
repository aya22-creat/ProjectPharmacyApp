using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Command.RemoveOrderItem
{

    public record RemoveOrderItemCommand(
        Guid OrderId,
        Guid OrderItemId
    ) : IRequest<OrderDto>;
}

