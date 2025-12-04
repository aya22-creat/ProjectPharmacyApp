using System;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Domain.OrderManagement.ValueObjects;
using PharmacyApp.Domain.OrderManagement.Enums;


namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order
{
    public void Confirm()
    {
        if (!State.CanTransitionTo(OrderStateEnum.Confirmed))
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

    public void Complete()
    {
        if (!State.CanTransitionTo(OrderStateEnum.Completed))
            throw new InvalidOperationException($"Cannot complete order in {State} state");

        if (State != OrderStateEnum.Delivered)
            throw new InvalidOperationException("Order must be delivered first");

        State = OrderStateEnum.Completed;
        CompletedAt = DateTime.UtcNow;
        RaiseDomainEvent(new OrderCompletedEvent(Id, CustomerId, TotalAmount.Amount, CompletedAt.Value));
    }
}
