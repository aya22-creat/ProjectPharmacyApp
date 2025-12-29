using PharmacyApp.Common.Common;
using MediatR;
using PharmacyApp.Domain.CartManagement.ValueObjects;

namespace PharmacyApp.Domain.CartManagement.Events;

public sealed class CartCheckedOutEvent : DomainEvent, INotification
{
    public Guid CartId { get; }
    public Guid CustomerId { get; }
    public decimal TotalAmount { get; }
    public string Currency { get; }
    public DateTime CheckedOutAt { get; } = DateTime.UtcNow;
    public IReadOnlyCollection<CartItemSnapshot> Items { get; }

    public CartCheckedOutEvent(
        Guid cartId,
        Guid customerId,
        decimal totalAmount,
        string currency,
        IEnumerable<CartItemSnapshot> items)
    {
        CartId = cartId;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        Currency = currency;
        Items = items.ToList().AsReadOnly();
    }
}
