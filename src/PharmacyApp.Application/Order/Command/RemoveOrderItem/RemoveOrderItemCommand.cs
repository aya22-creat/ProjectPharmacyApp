using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Commands.RemoveOrderItem
{
    
    public record RemoveOrderItemCommand(
        Guid OrderId,
        Guid OrderItemId
    ) : IRequest<OrderDto>;
}

