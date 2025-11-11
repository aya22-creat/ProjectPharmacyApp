
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
      );

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