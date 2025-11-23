using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.Command;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using CartEntity = PharmacyApp.Domain.CartManagement.Entities.Cart;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Application.Cart.Command.AddItem
{
    public class AddItemToCartCommandHandler : BaseCommandHandler<AddItemToCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepository;

        public AddItemToCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _cartRepository = cartRepository;
        }

        public override async Task<CartDto> Handle(AddItemToCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetActiveCartByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
            {
                cart = new CartEntity(request.CustomerId);
                await _cartRepository.AddAsync(cart, cancellationToken);
            }

            var unitPrice = Money.Create(request.UnitPrice, request.Currency ?? Constants.DefaultCurrency);
            cart.AddItem(request.ProductId, request.ProductName, request.Quantity, unitPrice);

            await SaveChangesAsync(cancellationToken);

            return new CartDto(cart);
        }
    }
}
