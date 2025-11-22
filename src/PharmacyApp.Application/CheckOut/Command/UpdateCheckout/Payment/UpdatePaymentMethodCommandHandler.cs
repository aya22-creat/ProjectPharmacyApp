using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Common;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Application.Order.Command.CreateOrder;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;

namespace PharmacyApp.Application.CheckOut.Command.UpdateCheckout.Payment
{
    public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, CheckoutDto>
    {
        private readonly ICheckOutRepository _checkoutRepository;

        public UpdatePaymentMethodCommandHandler(ICheckOutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<CheckoutDto> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken)
                ?? throw new InvalidOperationException("Checkout not found");

            var newPaymentMethod = new PaymentMethod(
                request.NewPaymentMethod.Type,
                request.NewPaymentMethod.Details
            );


            await _checkoutRepository.UpdateAsync(checkout, cancellationToken);

            return new CheckoutDto(checkout);
        }
    }
}