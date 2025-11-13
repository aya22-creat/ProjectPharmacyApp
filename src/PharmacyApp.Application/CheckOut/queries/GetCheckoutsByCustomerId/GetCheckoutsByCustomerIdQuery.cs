using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Queries.GetCheckoutsByCustomerId
{
    public record GetCheckoutsByCustomerIdQuery(Guid customerId) : IRequest<List<CheckoutDto>>;
}

