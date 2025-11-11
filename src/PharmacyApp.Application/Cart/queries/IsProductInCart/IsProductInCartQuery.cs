
using MediatR;

namespace PharmacyApp.Application.Cart.Queries.IsProductInCart
{
    public record IsProductInCartQuery(Guid CustomerId, Guid ProductId) : IRequest<bool>;
}