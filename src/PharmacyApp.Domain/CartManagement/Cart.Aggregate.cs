using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Events;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Domain.CartManagement;

public partial class Cart
{
    public static Cart Create(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId cannot be empty", nameof(customerId));

        return new Cart(customerId);
    }

    public Guid? OrderId { get; private set; }

    public void LinkOrder(Guid orderId)
    {
        if (OrderId != null)
            throw new InvalidOperationException("Cart is already linked to an order.");

        OrderId = orderId;
        UpdatedAt = DateTime.UtcNow;
    }

    

    public void AddItem(Guid productId, string productName, int quantity, Money price)
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot add items to inactive cart");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        var item = _items.FirstOrDefault(i => i.ProductId == productId);

        if (item is null)
        {
            item = new CartItem(productId, productName, quantity, price);
            _items.Add(item);
        }
        else
        {
            item.IncreaseQuantity(quantity);
        }

        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CartItemAddedEvent(Id, productId, quantity));
    }

    public bool RemoveItem(Guid cartItemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item is null)
            return false;

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new CartItemRemovedEvent(Id, item.ProductId, item.Quantity));
        return true;
    }

    public void UpdateItemQuantity(Guid cartItemId, int newQuantity)
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot update items in inactive cart");

        if (newQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(newQuantity));

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);
        if (item is null)
            return;

        var oldQuantity = item.Quantity;
        item.UpdateQuantity(newQuantity);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(
            new CartItemQuantityUpdatedEvent(Id, item.ProductId, cartItemId, newQuantity, oldQuantity)
        );
    }

    public void ClearCart()
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot clear inactive cart");

         foreach (var item in _items)
            AddDomainEvent(new CartItemRemovedEvent(Id, item.ProductId, item.Quantity));

        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }

 
    public void Activate()
    {
        if (State == CartStateEnum.Active)
            return;

        State = CartStateEnum.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (State == CartStateEnum.Inactive)
            return;

        State = CartStateEnum.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

   
    public Money GetTotal()
    {
        return _items.Count == 0
            ? Money.Zero("EGP")
            : _items.Aggregate(
                Money.Zero("EGP"),
                (sum, item) => sum.Add(item.GetSubtotal()));
    }

    public int GetTotalItemsCount() => _items.Sum(i => i.Quantity);
    public bool IsEmpty() => _items.Count == 0;
    public bool ContainsProduct(Guid productId) => _items.Any(i => i.ProductId == productId);
    public CartItem? GetItemByProductId(Guid productId)
        => _items.FirstOrDefault(i => i.ProductId == productId);


    public void Checkout()
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot checkout inactive cart");

        if (_items.Count == 0)
            throw new InvalidOperationException("Cannot checkout empty cart");

        State = CartStateEnum.Inactive;
        UpdatedAt = DateTime.UtcNow;

        var total = GetTotal();

        var itemsSnapshot = _items
            .Select(i => new CartItemSnapshot(
                i.ProductId,
                i.ProductName,
                i.Quantity,
                i.Price))
            .ToList();

        AddDomainEvent(
            new CartCheckedOutEvent(
                Id,
                CustomerId,
                total.Amount,
                total.Currency!,
                itemsSnapshot
            )
        );
    }
}
