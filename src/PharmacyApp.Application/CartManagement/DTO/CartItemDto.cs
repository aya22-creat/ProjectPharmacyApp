
using PharmacyApp.Domain.CartManagement.Entities;

namespace PharmacyApp.Application.CartManagement.DTO;

    //output cart item details (View cart || get cart)
    public record CartItemDto(
          Guid Id,
          Guid ProductId,
          string ProductName,
          decimal Price,
          int Quantity,
          decimal Tax,
          decimal Total,
          bool IsAvailable,
          int StockQuantity
      )
      {
          public CartItemDto(CartItem item) : this(
              item.Id,
              item.ProductId,
              item.ProductName,
              item.Price?.Amount ?? 0,
              item.Quantity,
              0, // Tax
              item.GetSubtotal().Amount,
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
       decimal Price,
       int Quantity
   );

    public record UpdateCartItemDto(
        Guid ProductId,
        int Quantity
    );
