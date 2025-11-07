using System;
using MediatR;
using PharmacyApp.Application.Order.DTO;

namespace PharmacyApp.Application.Order.queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}
