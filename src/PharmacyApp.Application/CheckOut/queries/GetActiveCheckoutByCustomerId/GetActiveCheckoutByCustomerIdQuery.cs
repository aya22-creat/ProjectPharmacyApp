using MediatR;
using PharmacyApp.Application.CheckOut.DTO;

namespace PharmacyApp.Application.CheckOut.Queries.GetActiveCheckoutByCustomerId
{
    public record GetActiveCheckoutByCustomerIdQuery(Guid CustomerId) : IRequest<CheckoutDto?>;
}