using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;

namespace PharmacyApp.Application.Cart.Queries.GetCartTotal
{
    public class GetCartTotalQueryHandler : IRequestHandler<GetCartTotalQuery, decimal>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartTotalQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<decimal> Handle(GetCartTotalQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
                return 0;

            var subTotal = cart.Items.Sum(i => i.GetSubtotal().Amount);
            var discount = cart.Items.Sum(i => i.Discount?.Amount ?? 0);
            var tax = subTotal * 0.14m;

            return subTotal - discount + tax;
        }
    }
}