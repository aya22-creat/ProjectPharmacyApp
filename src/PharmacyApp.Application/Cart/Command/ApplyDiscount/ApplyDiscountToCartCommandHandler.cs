
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Services;
using CartEntity = PharmacyApp.Domain.CartManagement.Cart;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Application.Cart.Command.ApplyDiscount
{
    public class ApplyDiscountToCartCommandHandler : BaseCommandHandler<ApplyDiscountToCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartCalculationService _cartCalculationService;

        public ApplyDiscountToCartCommandHandler(
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            ICartCalculationService cartCalculationService)
            : base(unitOfWork)
        {
            _cartRepository = cartRepository;
            _cartCalculationService = cartCalculationService;
        }

        public override async Task<CartDto> Handle(ApplyDiscountToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            // For now, apply a simple discount - in real implementation, this would validate against a coupon service
            var discountAmount = cart.GetTotal().Amount * Constants.DefaultDiscountRate;
            var discountMoney = Money.Create(discountAmount, cart.GetTotal().Currency ?? Constants.DefaultCurrency);

         

            await SaveChangesAsync(cancellationToken);

            return new CartDto(cart, _cartCalculationService);
        }
    }
}
