using CartEntity  = PharmacyApp.Domain.CartManagement.Cart;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.DTO
{
    //public record CartItemDto(Guid ProductId, string ProductName, int Quantity, decimal Price);

    public record CartDto(
        Guid Id,
        Guid CustomerId,
        IEnumerable<CartItemDto> Items,
        int TotalItems,
        decimal SubTotal,
        decimal Discount,
        decimal Tax,
        decimal TotalAmount,
        string Currency,
        DateTime CreatedAt,
        DateTime? UpdatedAt
    )
    {
        public CartDto(CartEntity  cart, ICartCalculationService calculationService) : this(
            cart.Id,
            cart.CustomerId,
            cart.Items.Select(item => new CartItemDto(item)),
            cart.GetTotalItemsCount(),
            cart.Items.Sum(i => i.GetSubtotal().Amount),
            cart.Discount?.Amount ?? 0m,
            calculationService.CalculateTax(cart.Items.Sum(i => i.GetSubtotal().Amount)),
            calculationService.CalculateTotal(cart),
            cart.GetTotal().Currency ?? "EGP",
            cart.CreatedAt,
            cart.UpdatedAt
        )
        {
        }
    }
}
