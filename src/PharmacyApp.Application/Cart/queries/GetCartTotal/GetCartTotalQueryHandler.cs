using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.Cart.Queries.GetCartTotal
{
    public class GetCartTotalQueryHandler : BaseQueryHandler<GetCartTotalQuery, decimal>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartCalculationService _cartCalculationService;

        public GetCartTotalQueryHandler(
            ICartRepository cartRepository,
            ICartCalculationService cartCalculationService)
        {
            _cartRepository = cartRepository;
            _cartCalculationService = cartCalculationService;
        }

        public override async Task<decimal> Handle(GetCartTotalQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
                return 0;

            return _cartCalculationService.CalculateTotal(cart);
        }
    }
}
