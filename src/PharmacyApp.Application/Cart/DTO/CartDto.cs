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
   );
}