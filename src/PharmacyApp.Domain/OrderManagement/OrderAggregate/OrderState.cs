using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Domain.OrderManagement.Enums;
using PharmacyApp.Domain.OrderManagement.Entities;
using  PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CartManagement.Entities;



namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order
{
public static Order CreateFromCart(Guid customerId, string shippingAddress, IEnumerable<CartItem> cartItems)
{
    if (customerId == Guid.Empty)
        throw new ArgumentException("Customer ID is required");

    if (string.IsNullOrWhiteSpace(shippingAddress))
        throw new ArgumentException("Shipping address is required");

    var order = new Order
    {
        Id = Guid.NewGuid(),
        CustomerId = customerId,
        CreatedAt = DateTime.UtcNow,
        State = OrderStateEnum.Created,
        ShippingAddress = shippingAddress
    };

    foreach (var it in cartItems)
    {
        order.AddItem(
            new OrderItem(it.ProductId, it.ProductName, it.Quantity, it.Price)
        );
    }

    order.AddDomainEvent(
        new OrderCreatedEvent(order.Id, customerId, order.GenerateOrderNumber(), order.TotalAmount.Amount)
    );

    return order;
}
    public void Confirm()
    {
        if (State is null || !State.CanTransitionTo(OrderStateEnum.Confirmed))
            throw new InvalidOperationException($"Cannot confirm order in {State} state");

        if (!_items.Any()) throw new InvalidOperationException("Cannot confirm order without items");

        State = OrderStateEnum.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
        RaiseDomainEvent(new OrderConfirmedEvent(Id, CustomerId, TotalAmount));
    }

    public void Cancel(string reason)
    {
        if (State == OrderStateEnum.Completed || State == OrderStateEnum.Cancelled)
            throw new InvalidOperationException("Cannot cancel completed or already cancelled order");

        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Cancellation reason required");

        State = OrderStateEnum.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationReason = reason;

        RaiseDomainEvent(new OrderCancelledEvent(Id, CustomerId, reason, CancelledAt.Value));
    }

    public void Reject(string reason)
{
      if (State is null || !State.CanTransitionTo(OrderStateEnum.Rejected))
        throw new InvalidOperationException($"Cannot reject order in {State} state");

    if (string.IsNullOrWhiteSpace(reason))
        throw new ArgumentException("Reason is required");

    if (State == OrderStateEnum.Completed ||
        State == OrderStateEnum.Cancelled ||
        State == OrderStateEnum.Rejected)
        throw new InvalidOperationException("Cannot reject an already finished or cancelled order");

    State = OrderStateEnum.Rejected;
    
    RejectedAt = DateTime.UtcNow;
    RejectionReason = reason;

    RaiseDomainEvent(new OrderRejectedEvent(Id, CustomerId, RejectedAt.Value, RejectionReason));
}


    public void Complete()
    {
        if (State is null || !State.CanTransitionTo(OrderStateEnum.Completed))
            throw new InvalidOperationException($"Cannot complete order in {State} state");

        if (State != OrderStateEnum.Delivered)
            throw new InvalidOperationException("Order must be delivered first");

        State = OrderStateEnum.Completed;
        CompletedAt = DateTime.UtcNow;
        RaiseDomainEvent(new OrderCompletedEvent(Id, CustomerId, TotalAmount.Amount, CompletedAt.Value));
    }
}       

