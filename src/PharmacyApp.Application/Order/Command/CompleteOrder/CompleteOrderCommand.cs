using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;


namespace PharmacyApp.Application.Order.Command.CompleteOrder
{
    public record CompleteOrderCommand(Guid OrderId) : IRequest<OrderDto>;
}