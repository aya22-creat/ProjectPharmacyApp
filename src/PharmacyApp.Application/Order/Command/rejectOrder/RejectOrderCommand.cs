using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;


namespace PharmacyApp.Application.Order.Command.RejectOrder
{
    public record RejectOrderCommand(Guid OrderId,Guid CustomerId,String Reason) : IRequest<OrderDto>;
}