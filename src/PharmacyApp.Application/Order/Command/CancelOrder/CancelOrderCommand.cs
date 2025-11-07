using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;


namespace PharmacyApp.Application.Order.Commands.CancelOrder
{
    public record CancelOrderCommand(Guid OrderId, string Reason) : IRequest<OrderDto>;
}

