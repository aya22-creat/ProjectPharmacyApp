using PharmacyApp.Common.Common;
using OrderStateEnum = PharmacyApp.Domain.OrderManagement.Enums.OrderStateEnum;
using PharmacyApp.Domain.OrderManagement.Entities;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.OrderManagement.Events;

namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order : AggregateRoot<Guid>
{
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

     public OrderStateEnum State { get; private set; }  



    public Guid CustomerId { get; private set; }
    public string OrderNumber { get; private set; }
    public DateTime OrderDate { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    
 public DateTime? RejectedAt { get; private set; }
    public string? CancellationReason { get; private set; }
    public string? RejectionReason { get; private set; }


    public string ShippingAddress { get; private set; }
    public string BillingAddress { get; private set; }
    public string PaymentMethod { get; private set; }

    public Money ShippingCost { get; private set; } = Money.Zero();
    public Money SubTotal => CalculateSubTotal();
    public Money TotalAmount => SubTotal.Add(ShippingCost);

    private Order() { } // EF

    public Order(
        Guid customerId,
        string shippingAddress,
        string billingAddress,
        string paymentMethod)
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        PaymentMethod = paymentMethod;

        OrderDate = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        OrderNumber = GenerateOrderNumber();
        State = OrderStateEnum.Processing;
    }

    public void AddItem(OrderItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        
        item.OrderId = Id; // for EF
        _items.Add(item);
    }

    private Money CalculateSubTotal() =>
        _items.Any()
            ? _items
                .Select(i => i.GetTotal())
                .Aggregate(Money.Zero(), (sum, next) => sum.Add(next))
            : Money.Zero();

    private string GenerateOrderNumber()
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var random = new Random().Next(1000, 9999);
        return $"ORD-{timestamp}-{random}";
    }
}
