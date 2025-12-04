
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Domain.CartManagement.Entities;
public class CartItem : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
    public Money Discount { get; set; }


    private CartItem() : base()
    {
        Price = Money.Zero("EGP");
        Discount = Money.Zero("EGP");
    }

    public CartItem(Guid productId, string productName, int quantity, Money price) : base(Guid.NewGuid())
    {
        if (productId == Guid.Empty)
        throw new ArgumentException("ProductId cannot be empty", nameof(productId));
        if (string.IsNullOrWhiteSpace(productName))
         throw new ArgumentException("ProductName cannot be empty", nameof(productName));
        if (quantity <= 0)
        throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Discount = Money.Zero("EGP");
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0) throw new ArgumentException("Quantity must be greater than zero", nameof(newQuantity));
        Quantity = newQuantity;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero", nameof(amount));
        Quantity += amount;
    }

    public Money GetSubtotal() => Price.Multiply(Quantity);
}