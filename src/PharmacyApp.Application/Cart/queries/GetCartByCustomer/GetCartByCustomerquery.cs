using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Queries.GetCartByCustomer
{
    public record GetCartByCustomerQuery(Guid CustomerId) : IRequest<CartDto?>;
}
