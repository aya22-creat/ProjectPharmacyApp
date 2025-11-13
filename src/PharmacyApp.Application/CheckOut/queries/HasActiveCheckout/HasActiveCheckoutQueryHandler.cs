using MediatR;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;

namespace PharmacyApp.Application.CheckOut.queries.HasActiveCheckout
{
    public class HasActiveCheckoutQueryHandler : IRequestHandler<HasActiveCheckoutQuery, bool>
    {
        private readonly ICheckOutRepository _checkOutRepository;

        public HasActiveCheckoutQueryHandler(ICheckOutRepository checkOutRepository)
        {
            _checkOutRepository = checkOutRepository;
        }

        public async Task<bool> Handle(HasActiveCheckoutQuery request, CancellationToken cancellationToken)
        {
            var checkout = await _checkOutRepository.GetActiveByCustomerIdAsync(request.CustomerId, cancellationToken);

            return checkout != null;
        }
    }
}
