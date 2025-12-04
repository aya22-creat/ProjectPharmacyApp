using Ardalis.SmartEnum;

namespace PharmacyApp.Domain.OrderManagement.Enums;

public sealed class OrderStateEnum : SmartEnum<OrderStateEnum, int>
{
    public static readonly OrderStateEnum Created     = new(nameof(Created), 1);
    public static readonly OrderStateEnum Confirmed   = new(nameof(Confirmed), 2);
    public static readonly OrderStateEnum Processing  = new(nameof(Processing), 3);
    public static readonly OrderStateEnum Delivered   = new(nameof(Delivered), 4);
    public static readonly OrderStateEnum Completed   = new(nameof(Completed), 5);
    public static readonly OrderStateEnum Cancelled   = new(nameof(Cancelled), 6);
    public static readonly OrderStateEnum Reserved    = new(nameof(Reserved), 7);

    private OrderStateEnum(string name, int value) : base(name, value) { }

    public bool CanTransitionTo(OrderStateEnum nextStatus)
    {
        return this switch
        {
            var s when s == Created   => nextStatus == Confirmed || nextStatus == Cancelled,
            var s when s == Confirmed => nextStatus == Processing || nextStatus == Cancelled,
            var s when s == Processing => nextStatus == Delivered || nextStatus == Cancelled,
            var s when s == Delivered => nextStatus == Completed,
            var s when s == Reserved => nextStatus == Processing || nextStatus == Cancelled,
            _ => false
        };
    }
}
