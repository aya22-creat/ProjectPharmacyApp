using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;


namespace PharmacyApp.Application.Cart.Queries.IsProductInCart
{
    public class IsProductInCartQueryHandler : IRequestHandler<IsProductInCartQuery, bool>
    {
        private readonly ICartRepository _cartRepository;

        public IsProductInCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(IsProductInCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
                return false;

            return cart.Items.Any(i => i.ProductId == request.ProductId);
        }
    }
}