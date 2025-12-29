using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Domain.CartManagement.ValueObjects;

public sealed class CartItemSnapshot
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public Money Price { get; }

    public CartItemSnapshot(
        Guid productId,
        string productName,
        int quantity,
        Money price)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }
}
