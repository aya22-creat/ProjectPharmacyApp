
using PharmacyApp.Domain.OrderManagement.Enums;
using PharmacyApp.Domain.OrderManagement.Entities;
using PharmacyApp.Domain.OrderManagement.ValueObjects;



namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order
{
    public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        if (State != OrderStateEnum.Created)
            throw new InvalidOperationException($"Cannot add items while order state is {State.Name}");

        var existing = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existing != null)
        {
            existing.UpdateQuantity(existing.Quantity + quantity);
        }
        else
        {
            var item = new OrderItem(productId, productName, quantity, unitPrice);
            _items.Add(item);
        }

    }

    public void RemoveItem(Guid orderItemId)
    {
        if (State != OrderStateEnum.Created)
            throw new InvalidOperationException($"Cannot remove items while order state is {State.Name}");

        var item = _items.FirstOrDefault(i => i.Id == orderItemId);

        if (item == null)
            throw new InvalidOperationException("Order item not found");

        _items.Remove(item);

    }
}
