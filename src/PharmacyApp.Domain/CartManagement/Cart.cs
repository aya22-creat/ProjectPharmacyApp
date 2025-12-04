using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CartManagement.ValueObjects;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Entities;
using System.Reflection;

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
    }
    public Guid CustomerId { get; private set; }
    public CartStateEnum State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string CouponCode { get; private set; } = string.Empty;
    public Money Discount { get; private set; } = Money.Zero("EGP");
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
}