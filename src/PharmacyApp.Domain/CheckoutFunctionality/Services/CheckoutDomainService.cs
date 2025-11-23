using System;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CheckoutFunctionality.Entities;
using PharmacyApp.Domain.CheckoutFunctionality.ValueObjects;

namespace PharmacyApp.Domain.CheckoutFunctionality.Services
{
    public class CheckoutDomainService
    {
        private readonly ICartRepository _cartRepository;

        public CheckoutDomainService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CheckoutAggregate> CreateCheckoutAsync(
            Guid customerId,
            Guid cartId,
            Address shippingAddress,
            Address billingAddress,
            PaymentMethod paymentMethod,
            CancellationToken cancellationToken = default)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId, cancellationToken);

            if (cart == null)
                throw new DomainException("Cart not found.");

            if (cart.IsEmpty())
                throw new DomainException("Cannot checkout an empty cart.");

            var total = cart.GetTotal();

            return CheckoutAggregate.Create(customerId, cartId, shippingAddress, billingAddress, paymentMethod, total);
        }
    }
}
