using System;
using MediatR;
using PharmacyApp.Application.OrderManagement.DTO;

namespace PharmacyApp.Application.OrderManagement.queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;
}
