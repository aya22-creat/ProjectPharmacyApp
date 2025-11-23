using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.Cart.Queries.GetCartItemsCount
{
    public class GetCartItemsCountQueryHandler : IRequestHandler<GetCartItemsCountQuery, int>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartItemsCountQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<int> Handle(GetCartItemsCountQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);


            if (cart == null)
                return 0;

            return cart.Items.Sum(i => i.Quantity);
        }
    }
}
