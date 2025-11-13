using MediatR;
using PharmacyApp.Application.CheckOut.DTO;

namespace PharmacyApp.Application.CheckOut.Queries.GetCheckoutSummary
{
        public record GetCheckoutSummaryQuery( Guid CheckoutId) : IRequest<CheckoutSummaryDto>;
}