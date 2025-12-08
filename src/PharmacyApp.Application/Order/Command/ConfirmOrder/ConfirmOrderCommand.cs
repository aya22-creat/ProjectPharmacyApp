using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Command.ConfirmOrder;

public record ConfirmOrderCommand(Guid OrderId, Guid AdminId) : IRequest<OrderDto>;
