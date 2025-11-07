using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.Queries.GetCustomerOrders
{
    public record GetCustomerOrdersQuery(Guid CustomerId) : IRequest<List<OrderDto>>;
}
