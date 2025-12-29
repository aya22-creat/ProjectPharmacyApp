using PharmacyApp.Domain.CartManagement.Entities;

namespace PharmacyApp.Application.CartManagement.DTO
{
    // Output cart item details (View cart || Get cart)
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

    // Input cart item details (Add to cart)
    public record AddToCartItemDto(
        Guid ProductId,
        string ProductName,
        decimal Price,
        int Quantity
    );

    // Input cart item details for update
    public record UpdateCartItemDto(
        Guid ProductId,
        int Quantity
    );

    // Output for quantity added recently
    public record AddedCartItemDto(
        Guid ProductId,
        string ProductName,
        int QuantityAdded,
        decimal TotalAdded
    );
}
