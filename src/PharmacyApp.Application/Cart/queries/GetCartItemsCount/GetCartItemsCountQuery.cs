using MediatR;


namespace PharmacyApp.Application.Cart.Queries.GetCartItemsCount
{
    public record GetCartItemsCountQuery(Guid CustomerId) : IRequest<int>;
}