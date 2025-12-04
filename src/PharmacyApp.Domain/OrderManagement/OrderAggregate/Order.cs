using PharmacyApp.Common.Common;
using OrderStateEnum = PharmacyApp.Domain.OrderManagement.Enums.OrderStateEnum;
using PharmacyApp.Domain.OrderManagement.Entities;
using PharmacyApp.Domain.OrderManagement.ValueObjects;
using PharmacyApp.Domain.OrderManagement.Events;

namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order : AggregateRoot<Guid>
{
   
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Guid CustomerId { get; private set; }
    public string OrderNumber { get; private set; }
    public OrderStateEnum State { get; private set; }
    public DateTime OrderDate { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }
    public string ShippingAddress { get; private set; }
    public string BillingAddress { get; private set; }
    public string PaymentMethod { get; private set; }

    public Money SubTotal => CalculateSubTotal();
    public Money ShippingCost { get; private set; } = Money.Zero();
    public Money Tax { get; private set; } = Money.Zero();
    public Money Discount { get; private set; } = Money.Zero();
    public Money TotalAmount => SubTotal.Add(ShippingCost).Add(Tax).Subtract(Discount);

    // Constructor
    public Order(Guid customerId, string shippingAddress, string billingAddress, string paymentMethod)
    {
        if (customerId == Guid.Empty) throw new ArgumentException("Customer ID is required");
        if (string.IsNullOrWhiteSpace(shippingAddress)) throw new ArgumentException("Shipping address is required");

        Id = Guid.NewGuid();
        CustomerId = customerId;
        OrderNumber = GenerateOrderNumber();
        State = OrderStateEnum.Created;
        ShippingAddress = shippingAddress;
        BillingAddress = string.IsNullOrWhiteSpace(billingAddress) ? shippingAddress : billingAddress;
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new OrderCreatedEvent(Id, CustomerId, OrderNumber, TotalAmount.Amount));
    }

    private Money CalculateSubTotal() =>
        _items.Any() ? _items.Select(static i => i.GetTotal()).Aggregate(static (sum, next) => sum.Add(next)) : Money.Zero();

    private string GenerateOrderNumber()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var random = new Random().Next(1000, 9999);
        return $"ORD-{timestamp}-{random}";
    }
}
