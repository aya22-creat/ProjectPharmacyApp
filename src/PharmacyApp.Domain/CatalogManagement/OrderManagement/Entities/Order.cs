
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Enums;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Events;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancellationReason { get; private set; }
        public string OrderNumber { get; private set; } = string.Empty;

        private Order()
        {
            OrderItems = new List<OrderItem>();
        }

        private Order(Guid customerId, DateTime orderDate, List<OrderItem> orderItems, decimal totalAmount, OrderStatus status)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = orderDate;
            OrderItems = orderItems ?? new List<OrderItem>();
            TotalAmount = totalAmount;
            Status = status;
            OrderNumber = GenerateOrderNumber();
        }

        //  Factory method for creating a new order
        public static Order Create(Guid customerId, List<OrderItem> orderItems)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Customer ID cannot be empty", nameof(customerId));

            if (orderItems == null || !orderItems.Any())
                throw new ArgumentException("Order must have at least one item", nameof(orderItems));

            var orderDate = DateTime.UtcNow;
            var totalAmount = orderItems.Sum(item => item.GetTotal().Amount);

            var order = new Order(customerId, orderDate, orderItems, totalAmount, OrderStatus.Pending);

            // Set OrderId on each order item
            order.OrderItems.ForEach(item => item.SetOrderId(order.Id));

            order.AddDomainEvent(new OrderCreatedEvent(order.Id, customerId, order.OrderNumber));

            return order;
        }

        //  Mark the order as completed
        public void Complete()
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException($"Cannot complete order with status: {Status}");

            Status = OrderStatus.Completed;
            CompletedAt = DateTime.UtcNow;


            AddDomainEvent(new OrderCompletedEvent(Id, TotalAmount, CompletedAt.Value));
        }

        public void Cancel(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Cancellation reason is required", nameof(reason));

            if (Status == OrderStatus.Completed)
                throw new InvalidOperationException("Cannot cancel a completed order.");

            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Order is already cancelled.");

            Status = OrderStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
            CancellationReason = reason;

            AddDomainEvent(new OrderCancelledEvent(Id, CustomerId, reason, CancelledAt.Value));
        }

        public void AddOrderItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot add items to a non-pending order.");

            OrderItems.Add(item);
            RecalculateTotal();
        }

        public void RemoveOrderItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot remove items from a non-pending order.");

            if (!OrderItems.Remove(item))
                throw new InvalidOperationException("Item not found in order.");

            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = OrderItems.Sum(item => item.GetTotal().Amount);
        }

        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }


}
