using MediatR;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Application.CheckOut.Queries.GetCheckout;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;




namespace PharmacyApp.Application.CheckOut.Queries.GetCheckout
{
    public class GetCheckoutByIdQueryHandler : IRequestHandler<GetCheckoutByIdQuery, CheckoutDto?>
    {
        private readonly ICheckOutRepository _checkoutRepository;

        public GetCheckoutByIdQueryHandler(ICheckOutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<CheckoutDto?> Handle(GetCheckoutByIdQuery request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken);

            return checkout == null ? null : new CheckoutDto(checkout);
        }
    }
}
