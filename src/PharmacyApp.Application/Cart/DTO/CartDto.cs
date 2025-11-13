using CartEntity = PharmacyApp.Domain.CatalogManagement.CartManagement.Entities.Cart;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;

namespace PharmacyApp.Application.Cart.DTO
{
    public record CartDto(
       Guid Id,
       Guid CustomerId,
       IEnumerable<CartItemDto> Items,
       int TotalItems,
       decimal SubTotal,
       decimal Discount,
       decimal Tax, // The tax amount varies depending on the price
       decimal TotalAmount,
       string Currency,
       DateTime CreatedAt,
       DateTime? UpdatedAt
   )
   {
       public CartDto(CartEntity cart) : this(
           cart.Id,
           cart.CustomerId,
           cart.Items.Select(item => new CartItemDto(item)),
           cart.GetTotalItemsCount(),
           cart.GetTotal().Amount,
           0, // Assuming no discount for now
           0, // Assuming no tax for now
           cart.GetTotal().Amount,
           cart.GetTotal().Currency ?? "EGP",
           cart.CreatedAt,
           cart.UpdatedAt
       )
       {
       }
   }
}
