using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Events;

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
    }



    // Add item to cart 
    public void AddItem(Guid productId, string productName, int quantity, Money price)
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot add items to inactive cart");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            var newItem = new CartItem(productId, productName, quantity, price);
            _items.Add(newItem);
        }
    }

    // Remove item from cart
    public void RemoveItem(Guid cartItemId)
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot remove items from inactive cart");

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);

        if (item == null)
            throw new InvalidOperationException($"CartItem with ID {cartItemId} not found");

        _items.Remove(item);

        AddDomainEvent(new CartItemRemovedEvent(Id, cartItemId, item.ProductId, item.Quantity));
    }

    // Update item quantity
    public void UpdateItemQuantity(Guid cartItemId, int newQuantity)
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot update items in inactive cart");

        var item = _items.FirstOrDefault(i => i.Id == cartItemId);

        if (item == null)
            throw new InvalidOperationException($"CartItem with ID {cartItemId} not found");

        item.UpdateQuantity(newQuantity);
    }

    // Clear all items from cart
    public void ClearCart()
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot clear inactive cart");

        _items.Clear();
    }

    public void Deactivate()
{
    State = CartStateEnum.Inactive;
}

public void Activate()
{
    State = CartStateEnum.Active;
}


    // Get cart total
    public Money GetTotal()
    {
        if (_items.Count == 0)
            return Money.Zero("EGP");

        var total = Money.Zero("EGP");

        foreach (var item in _items)
        {
            total = total.Add(item.GetSubtotal());
        }


        return total;
    }

    // Get total items count
    public int GetTotalItemsCount()
    {
        return _items.Sum(static i => i.Quantity);
    }

    // Check if cart is empty
    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    // Check if cart contains product
    public bool ContainsProduct(Guid productId)
    {
        return _items.Any(i => i.ProductId == productId);
    }

    // Get item by product ID
    public CartItem? GetItemByProductId(Guid productId)
    {
        return _items.FirstOrDefault(i => i.ProductId == productId);
    }

    public void Checkout()
    {
        if (State != CartStateEnum.Active)
            throw new InvalidOperationException("Cannot perform operation on inactive cart");
        if (_items.Count == 0)
            throw new InvalidOperationException("Cannot checkout empty cart");
        State = CartStateEnum.Inactive;
        var total = GetTotal();
        AddDomainEvent(new CartCheckedOutEvent(Id, CustomerId, total.Amount, total.Currency!, _items));
    }
    
}