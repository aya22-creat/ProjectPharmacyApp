using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Queries.GetCheckout
{
    public record GetCheckoutByIdQuery(Guid CheckoutId) : IRequest<CheckoutDto?>;
}