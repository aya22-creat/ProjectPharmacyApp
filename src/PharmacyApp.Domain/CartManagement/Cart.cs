using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.ValueObjects;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Events;

namespace PharmacyApp.Domain.CartManagement;

public partial class Cart : AggregateRoot<Guid>
{
    private Cart(Guid customerId) : base(Guid.NewGuid())
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId cannot be empty", nameof(customerId));

        CustomerId = customerId;
        State = CartStateEnum.Active;
        CreatedAt = DateTime.UtcNow;
        AddDomainEvent(new CartCreatedEvent(Id, CustomerId, CreatedAt));
    }
    public Guid CustomerId { get; private set; }
    public CartStateEnum State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Items
    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
}
