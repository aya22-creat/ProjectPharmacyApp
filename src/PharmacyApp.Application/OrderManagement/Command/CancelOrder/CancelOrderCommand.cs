
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;


namespace PharmacyApp.Application.OrderManagement.Command.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, Guid CustomerId, string Reason) : IRequest<OrderDto>;
}

