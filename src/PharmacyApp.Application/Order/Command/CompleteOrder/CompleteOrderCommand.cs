using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;


namespace PharmacyApp.Application.Order.Commands.CompleteOrder
{
    public record CompleteOrderCommand(Guid OrderId) : IRequest<OrderDto>;
}