using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;

namespace PharmacyApp.Application.OrderManagement.Command.ConfirmOrder;

public record ConfirmOrderCommand(Guid OrderId, Guid AdminId) : IRequest<OrderDto>;
