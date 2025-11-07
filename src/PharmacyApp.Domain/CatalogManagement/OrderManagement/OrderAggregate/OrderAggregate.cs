using System;
using System.Collections.Generic;
using System.Linq;
using PharmacyApp.Common.Common;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Events;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate
{
    public class Order : AggregateRoot<Guid>
    {
        private readonly List<OrderItem> _items = new();

        public Guid CustomerId { get; private set; }
        public string OrderNumber { get; private set; } = string.Empty;
        public OrderStatus Status { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public Money SubTotal => CalculateSubTotal();
        public Money ShippingCost { get; private set; } = Money.Zero();
        public Money Tax { get; private set; } = Money.Zero();
        public Money Discount { get; private set; } = Money.Zero();
        public Money TotalAmount => CalculateTotalAmount();

        public string ShippingAddress { get; private set; } = string.Empty;
        public string BillingAddress { get; private set; } = string.Empty;
        public string PaymentMethod { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }
        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? ShippedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancellationReason { get; private set; }

        


        public Order(Guid customerId, string shippingAddress, string billingAddress, string paymentMethod)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("Customer ID is required");

            if (string.IsNullOrWhiteSpace(shippingAddress))
                throw new ArgumentException("Shipping address is required");

            CustomerId = customerId;
            OrderNumber = GenerateOrderNumber();
            Status = OrderStatus.Pending;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress ?? shippingAddress;
            PaymentMethod = paymentMethod;
            ShippingCost = Money.Zero();
            Tax = Money.Zero();
            Discount = Money.Zero();
            CreatedAt = DateTime.UtcNow;

            RaiseDomainEvent(new OrderCreatedEvent(Id, CustomerId, OrderNumber));

        }

        public void AddItem(Guid productId, string productName, int quantity, Money unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException($"Cannot add items to order in {Status} status");

            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {
                var newItem = new OrderItem(productId, productName, unitPrice, quantity);
                newItem.SetOrderId(Id);
                _items.Add(newItem);
            }

            RaiseDomainEvent(new OrderItemAddedEvent(Id, productId, quantity));
        }

        public void RemoveItem(Guid OrderItemId)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException($"Cannot remove items from order in {Status} status");

            var item = _items.FirstOrDefault(i => i.Id == OrderItemId);
            if (item == null)
                throw new InvalidOperationException($"Order item {OrderItemId} not found");

            _items.Remove(item);
            RaiseDomainEvent(new OrderItemRemovedEvent(Id, OrderItemId));
        }

        public void UpdateShippingCost(Money shippingCost)
        {
            if (shippingCost.Amount < 0)
                throw new ArgumentException("Shipping cost cannot be negative");

            ShippingCost = shippingCost;
        }

        public void ApplyTax(Money tax)
        {
            if (tax.Amount < 0)
                throw new ArgumentException("Tax cannot be negative");

            Tax = tax;
        }

        public void ApplyDiscount(Money discount)
        {
            if (discount.Amount < 0)
                throw new ArgumentException("Discount cannot be negative");

            if (discount.Amount > SubTotal.Amount)
                throw new ArgumentException("Discount cannot exceed subtotal");

            Discount = discount;
        }

        public void Confirm()
        {
            if (!Status.CanTransitionTo(OrderStatus.Confirmed))
                throw new InvalidOperationException($"Cannot confirm order in {Status} status");

            if (!_items.Any())
                throw new InvalidOperationException("Cannot confirm order without items");

            Status = OrderStatus.Confirmed;
            ConfirmedAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderConfirmedEvent(Id, CustomerId, TotalAmount));
        }
        public void Complete()
        {
            if (!Status.CanTransitionTo(OrderStatus.Completed))
                throw new InvalidOperationException($"Cannot complete order in {Status} status");

            if (Status != OrderStatus.Delivered)
                throw new InvalidOperationException("Order must be delivered before completion");

            Status = OrderStatus.Completed;
            CompletedAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderCompletedEvent(Id, TotalAmount.Amount, CompletedAt.Value));
        }

        public void StartProcessing()
        {
            if (!Status.CanTransitionTo(OrderStatus.Processing))
                throw new InvalidOperationException($"Cannot start processing order in {Status} status");

            Status = OrderStatus.Processing;
            RaiseDomainEvent(new OrderProcessingStartedEvent(Id));
        }

        public void Ship()
        {
            if (!Status.CanTransitionTo(OrderStatus.Shipping))
                throw new InvalidOperationException($"Cannot ship order in {Status} status");

            Status = OrderStatus.Shipping;
            ShippedAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderShippedEvent(Id, CustomerId, ShippingAddress));
        }

        public void MarkAsDelivered()
        {
            if (!Status.CanTransitionTo(OrderStatus.Delivered))
                throw new InvalidOperationException($"Cannot mark order as delivered in {Status} status");

            Status = OrderStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
            RaiseDomainEvent(new OrderDeliveredEvent(Id, CustomerId));
        }

        public void Cancel(string reason)
        {
            if (!Status.CanTransitionTo(OrderStatus.Cancelled))
                throw new InvalidOperationException($"Cannot cancel order in {Status} status");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Cancellation reason is required");

            Status = OrderStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
            CancellationReason = reason;
            RaiseDomainEvent(new OrderCancelledEvent(Id, CustomerId, reason, CancelledAt.Value));
        }

        public void Update(string reason)
        {
            if (!Status.CanTransitionTo(OrderStatus.Processing))
                throw new InvalidOperationException($"Cannot update order in {Status} status");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Update reason is required");

            // Assuming update means starting processing or something similar
            Status = OrderStatus.Processing;
            RaiseDomainEvent(new OrderProcessingStartedEvent(Id));
        }

        private Money CalculateSubTotal()
        {
            if (!_items.Any())
                return Money.Zero();

            return _items
                .Select(i => i.GetTotal())
                .Aggregate((sum, next) => sum.Add(next));
        }

        private Money CalculateTotalAmount()
        {
            var total = SubTotal.Add(ShippingCost).Add(Tax).Subtract(Discount);
            return total;
        }
//Unique Order Number
        private string GenerateOrderNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }
}
