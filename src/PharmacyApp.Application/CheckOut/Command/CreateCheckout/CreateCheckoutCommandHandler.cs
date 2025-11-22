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

namespace PharmacyApp.Application.CheckOut.Command.CreateCheckout
{
     public class CreateCheckoutCommandHandler : IRequestHandler<CreateCheckoutCommand, CheckoutDto>
    {
        private readonly ICheckOutRepository _checkoutRepository;
        private readonly ICartRepository _cartRepository;

        public CreateCheckoutCommandHandler(
            ICheckOutRepository checkoutRepository,
            ICartRepository cartRepository)
        {
            _checkoutRepository = checkoutRepository;
            _cartRepository = cartRepository;
        }
        public async Task<CheckoutDto> Handle(CreateCheckoutCommand request, CancellationToken cancellationToken)
        {
          var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            if (cart.CustomerId != request.CustomerId)
                throw new InvalidOperationException("Cart does not belong to this customer");

            if (cart.IsEmpty())
                throw new InvalidOperationException("Cannot checkout an empty cart");

            var shippingAddress = new Address(
                request.ShippingAddress.Street,
                request.ShippingAddress.City,
                request.ShippingAddress.Country,
                request.ShippingAddress.ZipCode
            );

            var billingAddress = new Address(
                request.BillingAddress.Street,
                request.BillingAddress.City,
                request.BillingAddress.Country,
                request.BillingAddress.ZipCode
            );

            var paymentMethod = new PaymentMethod(
                request.PaymentMethod.Type,
                request.PaymentMethod.Details
            );

            var totalPrice = cart.GetTotal();

            var checkout = CheckoutAggregate.Create(
                request.CustomerId,
                request.CartId,
                shippingAddress,
                billingAddress,
                paymentMethod,
                totalPrice
            );

            await _checkoutRepository.AddAsync(checkout, cancellationToken);

            return new CheckoutDto(checkout);


        }
    }}
