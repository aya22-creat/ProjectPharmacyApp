using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;

namespace PharmacyApp.Application.OrderManagement.Queries.GetCustomerOrders
{
    public record GetCustomerOrdersQuery(Guid CustomerId) : IRequest<List<OrderDto>>;
}
