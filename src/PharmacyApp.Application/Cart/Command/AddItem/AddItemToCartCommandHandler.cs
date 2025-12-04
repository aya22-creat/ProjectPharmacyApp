
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using CartEntity = PharmacyApp.Domain.CartManagement.Cart;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Application.Cart.Command.AddItem
{
    public class AddItemToCartCommandHandler : BaseCommandHandler<AddItemToCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartCalculationService _cartCalculationService;

        public AddItemToCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ICartCalculationService cartCalculationService)
            : base(unitOfWork)
        {
            _cartRepository = cartRepository;
            _cartCalculationService = cartCalculationService;
        }

        public override async Task<CartDto> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
            {
                cart = CartEntity.Create(request.CustomerId);
                await _cartRepository.AddAsync(cart, cancellationToken);
            }

            var unitPrice = Money.Create(request.UnitPrice, request.Currency ?? Constants.DefaultCurrency);
            cart.AddItem(request.ProductId, request.ProductName, request.Quantity, unitPrice);

            await SaveChangesAsync(cancellationToken);

            var subtotal = cart.Items.Sum(static i => i.GetSubtotal().Amount);
            var discount = cart.Discount?.Amount ?? 0;
            var tax = _cartCalculationService.CalculateTax(subtotal);
            var totalAmount = subtotal - discount + tax;

            return new CartDto(
                Id: cart.Id,
                CustomerId: cart.CustomerId,
                Items: cart.Items.Select(static item => new CartItemDto(item)),
                TotalItems: cart.GetTotalItemsCount(),
                SubTotal: subtotal,
                Discount: discount,
                Tax: tax,
                TotalAmount: totalAmount,
                Currency: cart.GetTotal().Currency ?? Constants.DefaultCurrency,
                CreatedAt: cart.CreatedAt,
                UpdatedAt: cart.UpdatedAt
            );
        }
    }
}
