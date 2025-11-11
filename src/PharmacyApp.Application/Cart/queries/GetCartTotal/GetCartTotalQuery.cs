using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Queries.GetCartTotal
{
    public record GetCartTotalQuery(Guid CustomerId) : IRequest<decimal>;
}