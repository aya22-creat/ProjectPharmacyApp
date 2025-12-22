
using MediatR;

namespace PharmacyApp.Application.CartManagement.Queries.IsProductInCart
{
    public record IsProductInCartQuery(Guid CustomerId, Guid ProductId) : IRequest<bool>;
}