using MediatR;


namespace PharmacyApp.Application.CartManagement.Queries.GetCartItemsCount
{
    public record GetCartItemsCountQuery(Guid CustomerId) : IRequest<int>;
}