using MediatR;
using PharmacyApp.Application.Order.DTO;


namespace PharmacyApp.Application.Order.Command.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, string Reason) : IRequest<OrderDto>;
}

