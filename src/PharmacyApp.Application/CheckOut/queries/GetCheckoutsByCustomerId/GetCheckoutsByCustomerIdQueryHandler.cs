using MediatR;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Domain.CheckoutFunctionality.Repositories;

namespace PharmacyApp.Application.CheckOut.Queries.GetCheckoutsByCustomerId
{
    public class GetCheckoutsByCustomerIdQueryHandler : IRequestHandler<GetCheckoutsByCustomerIdQuery, List<CheckoutDto>>
    {
        private readonly ICheckOutRepository _checkoutRepository;

        public GetCheckoutsByCustomerIdQueryHandler(ICheckOutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<List<CheckoutDto>> Handle(GetCheckoutsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var checkouts = await _checkoutRepository.GetByCustomerIdAsync(request.customerId, cancellationToken);

            return checkouts.Select(c => new CheckoutDto(c)).ToList();
        }
    }
}
