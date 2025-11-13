using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.queries.HasActiveCheckout
{
    public record HasActiveCheckoutQuery(Guid CustomerId) : IRequest<bool>;
}