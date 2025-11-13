using MediatR;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Services;


namespace PharmacyApp.Application.CheckOut.Queries.GetActiveCheckoutByCustomerId
{  public class GetActiveCheckoutByCustomerIdQueryHandler : IRequestHandler<GetActiveCheckoutByCustomerIdQuery, CheckoutDto?>
    {
        private readonly ICheckOutRepository _checkoutRepository;

        public GetActiveCheckoutByCustomerIdQueryHandler(ICheckOutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<CheckoutDto?> Handle(GetActiveCheckoutByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetActiveByCustomerIdAsync(request.CustomerId, cancellationToken);

            return checkout == null ? null : new CheckoutDto(checkout);
        }
    }
}
