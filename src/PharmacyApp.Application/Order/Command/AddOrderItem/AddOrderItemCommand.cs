using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Commands.AddOrderItem
{
    public record AddOrderItemCommand(
        Guid OrderId,
        Guid ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice
    ) : IRequest<OrderDto>;
}