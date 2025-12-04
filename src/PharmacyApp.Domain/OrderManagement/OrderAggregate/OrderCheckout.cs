using System;
using System.Linq;
using PharmacyApp.Domain.OrderManagement.Events;
using PharmacyApp.Domain.OrderManagement.Enums;

namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order
{
    public void Checkout()
    {
        if (!State.CanTransitionTo(OrderStateEnum.Completed))
            throw new InvalidOperationException($"Cannot checkout when state is {State.Name}");

        if (!_items.Any())
            throw new InvalidOperationException("Cannot checkout an empty order");

    
        State = OrderStateEnum.Completed;
        ConfirmedAt = DateTime.UtcNow;

        var totalAmount = TotalAmount.Amount;

        RaiseDomainEvent(
            new OrderCheckedOutEvent(Id, CustomerId, totalAmount)
        );
    }
}
