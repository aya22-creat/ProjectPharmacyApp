using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Services;

namespace PharmacyApp.Application.Cart.Queries.GetCartByCustomer
{
    public class GetCartByCustomerQueryHandler : BaseQueryHandler<GetCartByCustomerQuery, CartDto?>
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartCalculationService _cartCalculationService;

        public GetCartByCustomerQueryHandler(
            ICartRepository cartRepository,
            ICartCalculationService cartCalculationService)
        {
            _cartRepository = cartRepository;
            _cartCalculationService = cartCalculationService;
        }

        public override async Task<CartDto?> Handle(GetCartByCustomerQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
                return null;

            var items = cart.Items
               .Select(i => new CartItemDto(
                   Id: i.Id,
                   ProductId: i.ProductId,
                   ProductName: i.ProductName,
                   ProductImage: null,
                   UnitPrice: i.Price!.Amount,
                   Quantity: i.Quantity,
                   Discount: 0,
                   Tax: 0,
                   Total: i.GetSubtotal().Amount,
                   IsAvailable: Constants.DefaultAvailability,
                   StockQuantity: Constants.DefaultStockQuantity
               ))
               .ToList();

            var subTotal = items.Sum(x => x.Total);
            var totalQuantity = items.Sum(x => x.Quantity);
            var discount = 0m;
            var tax = _cartCalculationService.CalculateTax(subTotal);
            var grandTotal = subTotal - discount + tax;

            //Separation of Concerns
            var cartDto = new CartDto(
                Id: cart.Id,
                CustomerId: cart.CustomerId,
                Items: items,
                TotalItems: totalQuantity,
                SubTotal: subTotal,
                Discount: discount,
                Tax: tax,
                TotalAmount: grandTotal,
                Currency: Constants.DefaultCurrency,
                CreatedAt: cart.CreatedAt,
                UpdatedAt: cart.UpdatedAt
            );

            return cartDto;
        }
    }
}
