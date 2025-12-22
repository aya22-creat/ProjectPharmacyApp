using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;


namespace PharmacyApp.Application.OrderManagement.Command.CompleteOrder
{
    public record CompleteOrderCommand(Guid OrderId) : IRequest<OrderDto>;
}