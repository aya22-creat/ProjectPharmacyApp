using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Domain.OrderManagement.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; internal set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money Price { get; private set; } = null!;

    private OrderItem() { } // EF Core

    public OrderItem(Guid productId, string productName, int quantity, Money price)
    {
        if (productId == Guid.Empty) throw new ArgumentException("Product ID is required");
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Product name is required");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be greater than zero");
        Quantity = newQuantity;
    }

    public Money GetTotal() => Price.Multiply(Quantity);
}
