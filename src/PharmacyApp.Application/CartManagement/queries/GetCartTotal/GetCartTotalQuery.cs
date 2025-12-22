using System;
using System.Collections.Generic;
using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Queries.GetCartTotal
{
    public record GetCartTotalQuery(Guid CustomerId) : IRequest<decimal>;
}