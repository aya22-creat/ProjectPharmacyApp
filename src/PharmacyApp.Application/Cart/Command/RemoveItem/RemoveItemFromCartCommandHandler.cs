using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Services;

namespace PharmacyApp.Application.Cart.Command.RemoveItem
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartCalculationService _cartCalculationService;

        public RemoveItemFromCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ICartCalculationService cartCalculationService)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _cartCalculationService = cartCalculationService;
        }

        public async Task<CartDto> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            cart.RemoveItem(request.CartItemId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var subtotal = cart.Items.Sum(static i => i.GetSubtotal().Amount);
            var totalAmount = subtotal ;

            return new CartDto(
                Id: cart.Id,
                CustomerId: cart.CustomerId,
                Items: cart.Items.Select(static item => new CartItemDto(item)),
                TotalItems: cart.GetTotalItemsCount(),
                SubTotal: subtotal,
                TotalAmount: totalAmount,
                Currency: cart.GetTotal().Currency ?? Constants.DefaultCurrency,
                CreatedAt: cart.CreatedAt,
                UpdatedAt: cart.UpdatedAt
            );
        }
    }
}