using MediatR;
using PharmacyApp.Application.Cart.DTO;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Repositories;

namespace PharmacyApp.Application.Cart.Queries.GetCartByCustomer
{
    public class GetCartByCustomerQueryHandler : IRequestHandler<GetCartByCustomerQuery, CartDto?>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByCustomerQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto?> Handle(GetCartByCustomerQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart == null)
                return null;


            var items = cart.Items
               .Select(i => new CartItemDto(
                   Id: i.Id,
                   ProductId: i.ProductId,
                   ProductName: "",
                   ProductImage: null,
                   UnitPrice: i.Price!.Amount,
                   Quantity: i.Quantity,
                   Discount: 0,
                   Tax: 0,
                   Total: i.GetSubtotal().Amount,
                   IsAvailable: true,
                   StockQuantity: 100     //defualt value 
               ))
               .ToList();

            var subTotal = items.Sum(x => x.Total);
            var totalQuantity = items.Sum(x => x.Quantity);
            var discount = 0m;
            var tax = subTotal * 0.14m;
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
                Currency: "EGP",
                CreatedAt: cart.CreatedAt,
                UpdatedAt: cart.UpdatedAt
            );

            return cartDto;


        }
    }
}
