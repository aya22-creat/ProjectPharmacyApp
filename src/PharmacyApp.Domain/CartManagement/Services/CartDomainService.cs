using System;
using System.Threading;
using System.Threading.Tasks;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Common.Common.Repositories;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public class CartDomainService
    {
        private readonly ICartRepository _cartRepository;

        public CartDomainService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public async Task AddCartAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            await _cartRepository.AddAsync(cart, cancellationToken);
        }
    }
}
