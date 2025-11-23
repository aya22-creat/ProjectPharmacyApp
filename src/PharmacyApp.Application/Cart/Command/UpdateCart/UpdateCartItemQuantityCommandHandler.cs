using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.Command;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Order.Command.CreateOrder;
using PharmacyApp.Application.Order.DTO;
using PharmacyApp.Domain.CartManagement.Repositories;
using CartEntity = PharmacyApp.Domain.CartManagement.Entities.Cart;

namespace PharmacyApp.Application.Cart.Command.UpdateCart
{
    public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCartItemQuantityCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartDto> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            cart.UpdateItemQuantity(request.CartItemId, request.NewQuantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CartDto(cart);
        }
    }
}
