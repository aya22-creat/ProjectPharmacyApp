using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Common;
using CartEntity = PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities.CheckoutAggregate;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;

namespace PharmacyApp.Application.CheckOut.Command.CancelCheckout
{
    public class cancelCheckoutCommandHandler : IRequestHandler<CancelCheckoutCommand, Unit>
    {
        private readonly ICheckOutRepository _checkoutRepository;
        private readonly IUnitOfWork _unitOfWork;

        public cancelCheckoutCommandHandler(
            ICheckOutRepository checkoutRepository,
            IUnitOfWork unitOfWork)
        {
            _checkoutRepository = checkoutRepository;
            _unitOfWork = unitOfWork;
        }

         public async Task<Unit> Handle(CancelCheckoutCommand request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken)
                ?? throw new InvalidOperationException("Checkout not found");

            checkout.CancelCheckout();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}