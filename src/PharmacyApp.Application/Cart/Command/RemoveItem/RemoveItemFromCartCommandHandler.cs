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

namespace PharmacyApp.Application.Cart.Command.RemoveItem
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartDto> Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            cart.RemoveItem(request.CartItemId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CartDto(cart);
        }
    }
}