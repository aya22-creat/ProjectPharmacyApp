using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;


namespace PharmacyApp.Application.OrderManagement.Command.RejectOrder
{
    public record RejectOrderCommand(Guid OrderId,Guid CustomerId,String Reason) : IRequest<OrderDto>;
}