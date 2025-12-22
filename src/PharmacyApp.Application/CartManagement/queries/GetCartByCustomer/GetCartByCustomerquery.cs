using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Queries.GetCartByCustomer
{
    public record GetCartByCustomerQuery(Guid CustomerId) : IRequest<CartDto?>;
}
