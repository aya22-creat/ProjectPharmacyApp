using MediatR;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.Services;

using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Domain.CartManagement.Repositories;
using CartEntity =PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Application.Cart.Command.UpdateCart
{
    public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartCalculationService _cartCalculationService;

        public UpdateCartItemQuantityCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ICartCalculationService cartCalculationService)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _cartCalculationService = cartCalculationService;
        }

        public async Task<CartDto> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            cart.UpdateItemQuantity(request.CartItemId, request.NewQuantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var subtotal = cart.Items.Sum(static i => i.GetSubtotal().Amount);
            var totalAmount = subtotal;

            return new CartDto(
                Id: cart.Id,
                CustomerId: cart.CustomerId,
                Items: cart.Items.Select(static item => new CartItemDto(item)),
                TotalItems: cart.GetTotalItemsCount(),
                SubTotal: subtotal,
                TotalAmount: totalAmount,
                Currency: cart.GetTotal().Currency ?? "EGP",
                CreatedAt: cart.CreatedAt,
                UpdatedAt: cart.UpdatedAt
            );
        }
    }
}
