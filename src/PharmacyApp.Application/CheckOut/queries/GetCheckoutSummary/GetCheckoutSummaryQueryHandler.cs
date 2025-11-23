using MediatR;
using PharmacyApp.Application.CheckOut.DTO;
using PharmacyApp.Domain.CheckoutFunctionality.Repositories;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.CheckOut.Queries.GetCheckoutSummary
{
    public class GetCheckoutSummaryQueryHandler : IRequestHandler<GetCheckoutSummaryQuery, CheckoutSummaryDto>
    {
        private readonly ICheckOutRepository _checkoutRepository;
        private readonly ICartRepository _cartRepository;

        public GetCheckoutSummaryQueryHandler(
            ICheckOutRepository checkoutRepository,
            ICartRepository cartRepository)
        {
            _checkoutRepository = checkoutRepository;
            _cartRepository = cartRepository;
        }

        public async Task<CheckoutSummaryDto> Handle(GetCheckoutSummaryQuery request, CancellationToken cancellationToken)
        {
            var checkout = await _checkoutRepository.GetByIdAsync(request.CheckoutId, cancellationToken)
                ?? throw new InvalidOperationException("Checkout not found");

            var cart = await _cartRepository.GetByIdAsync(checkout.CartId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            var total = cart.GetTotal();
            var subTotal = total.Amount;
            var discount = cart.Discount.Amount;
            var shippingCost = 50m; // not static in real app 
            var tax = (subTotal - discount) * 0.14m;
            var totalAmount = subTotal - discount + shippingCost + tax;

            return new CheckoutSummaryDto(
                CheckoutId: checkout.Id,
                ItemsCount: cart.GetTotalItemsCount(),
                SubTotal: subTotal,
                ShippingCost: shippingCost,
                Tax: tax,
                Discount: discount,
                TotalAmount: totalAmount,
                Currency: checkout.TotalPrice.Currency ?? "EGP"
            );
        }
    }
}
