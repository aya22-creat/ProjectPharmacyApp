
using CartItemEntity = PharmacyApp.Domain.CartManagement.Entities.CartItem;
using PharmacyApp.Domain.CartManagement.Entities;

namespace PharmacyApp.Application.Cart.DTO
{
    //output cart item details (View cart || get cart)
    public record CartItemDto(
          Guid Id,
          Guid ProductId,
          string ProductName,
          string? ProductImage,
          decimal UnitPrice,
          int Quantity,
          decimal Discount,
          decimal Tax,
          decimal Total,
          bool IsAvailable,
          int StockQuantity
      )
      {
          public CartItemDto(CartItemEntity item) : this(
              item.Id,
              item.ProductId,
              item.ProductName,
              null, // ProductImage
              item.Price?.Amount ?? 0,
              item.Quantity,
              item.Discount?.Amount ?? 0,
              0, // Tax
              item.GetTotal().Amount,
              true, // IsAvailable
              0 // StockQuantity
          )
          {
          }
      }

    //input cart item details(Add to cart)

    public record AddToCartItemDto(
       Guid ProductId,
       string ProductName,
       decimal UnitPrice,
       int Quantity
   );

    public record UpdateCartItemDto(
        Guid ProductId,
        int Quantity
    );
}
