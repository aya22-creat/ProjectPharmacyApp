using PharmacyApp.Common.Common;
using PharmacyApp.Domain.OrderManagement.ValueObjects;

namespace PharmacyApp.Domain.OrderManagement.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = null!;
    public Money Total => UnitPrice.Multiply(Quantity);

    private OrderItem() { }

    public OrderItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (productId == Guid.Empty) throw new ArgumentException("Product ID is required");
        if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Product name is required");
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be greater than zero");
        Quantity = newQuantity;
    }

    public Money GetTotal() => Total;
}
