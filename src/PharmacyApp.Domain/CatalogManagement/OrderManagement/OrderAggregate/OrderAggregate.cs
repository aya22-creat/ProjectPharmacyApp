using System;
using System.Collections.Generic;
using System.Linq;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.OrderAggregate
{
    public class Order : AggregateRoot<Guid>
    {
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public string? OrderNumber { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus? Status { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }
        public DateTime? CancelledDate { get; private set; }
        public string? CancellationReason { get; private set; }


        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public string Currency { get; private set; } = string.Empty;
        public Money? ShippingFee { get; private set; }
        public Money? TaxAmount { get; private set; }
        public Money? DiscountAmount { get; private set; }
        public decimal? TaxRate { get; private set; }

        // add other properties like ShippingAddress, BillingAddress, etc. as needed



        private Order() { }

        private void ValidateCanModify()
        {
            if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
                throw new DomainException("Order cannot be modified when it's completed or cancelled.");
        }

        public bool CanModify()
        {
            return Status != OrderStatus.Completed && Status != OrderStatus.Cancelled;
        }

        public bool CanCancel()
        {
            return Status != OrderStatus.Completed;
        }

        public bool IsFinal()
        {
            return Status == OrderStatus.Completed || Status == OrderStatus.Cancelled;
        }

        public bool IsEmpty()
        {
            return !_orderItems.Any();
        }

        public Order(Guid orderId, string orderNumber, Guid customerId, DateTime createdDate, OrderStatus status)
        {

            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new DomainException("Order number cannot be empty.");

            if (customerId == Guid.Empty)
                throw new DomainException("Customer ID cannot be empty.");



            Id = Guid.NewGuid();
            OrderNumber = orderNumber;
            CustomerId = customerId;
            CreatedDate = createdDate;
            Status = status;
        }

        public static Order Create(string orderNumber, Guid customerId, OrderStatus status, string currency = "EGP")
        {
            var order = new Order(Guid.NewGuid(), orderNumber, customerId, DateTime.UtcNow, status);
            order.Currency = currency;
            return order;
        }




        public void AddItem(Guid productId, string productName, Money unitPrice, int quantity)
        {
            ValidateCanModify();

            if (productId == Guid.Empty)
                throw new DomainException("Product ID cannot be empty.");

            if (string.IsNullOrWhiteSpace(productName))
                throw new DomainException("Product name cannot be empty.");

            if (unitPrice == null)
                throw new ArgumentNullException(nameof(unitPrice));

            if (unitPrice.Currency != Currency)
                throw new DomainException(
                    $"Item currency ({unitPrice.Currency}) does not match order currency ({Currency}).");

            if (quantity <= 0)
                throw new DomainException("Quantity must be greater than zero.");

            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            }
            else
            {

                var orderItem = new OrderItem(
                    productId,
                    productName,
                    unitPrice,
                    quantity);

                _orderItems.Add(orderItem);
            }
        }

        // Overloaded method to add an existing OrderItem 
        public void AddItem(OrderItem item)
        {
            ValidateCanModify();

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.UnitPrice.Currency != Currency)
                throw new DomainException(
                    $"Item currency ({item.UnitPrice.Currency}) does not match order currency ({Currency})");

            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
            }
            else
            {
                _orderItems.Add(item);
            }
        }
        public void RemoveItem(Guid productId)
        {
            ValidateCanModify();

            var item = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new DomainException($"Item with product ID {productId} not found in order.");

            _orderItems.Remove(item);
        }
        public void UpdateItemQuantity(Guid productId, int newQuantity)
        {
            ValidateCanModify();

            var item = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new DomainException($"Item with product ID {productId} not found in order.");

            if (newQuantity <= 0)
            {
                RemoveItem(productId);
            }
            else
            {
                item.UpdateQuantity(newQuantity);
            }
        }
        public void ApplyItemDiscount(Guid productId, Money discountAmount)
        {
            ValidateCanModify();

            var item = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
                throw new DomainException($"Item with product ID {productId} not found in order.");

            item.ApplyDiscount(discountAmount);
        }
        public void ClearItems()
        {
            ValidateCanModify();
            _orderItems.Clear();
        }
        public void SetShippingFee(Money shippingFee)
        {
            ValidateCanModify();

            if (shippingFee == null)
                throw new ArgumentNullException(nameof(shippingFee));

            if (shippingFee.Currency != Currency)
                throw new DomainException(
                    $"Shipping fee currency ({shippingFee.Currency}) does not match order currency ({Currency})");

            ShippingFee = shippingFee;
        }

        public void CalculateTax(decimal taxRate)
        {
            ValidateCanModify();

            if (taxRate < 0 || taxRate > 1)
                throw new DomainException("Tax rate must be between 0 and 1 (e.g., 0.14 for 14%)");

            TaxRate = taxRate;
            var subtotal = GetSubtotal();
            TaxAmount = subtotal.CalculateTax(taxRate);
        }
        public void SetTax(decimal taxRate)
        {
            ValidateCanModify();

            if (taxRate < 0 || taxRate > 1)
                throw new DomainException("Tax rate must be between 0 and 1.");

            TaxRate = taxRate;

        }
        public void ApplyDiscount(Money discountAmount)
        {
            ValidateCanModify();

            if (discountAmount == null)
                throw new ArgumentNullException(nameof(discountAmount));

            if (discountAmount.Currency != Currency)
                throw new DomainException(
                    $"Discount currency ({discountAmount.Currency}) does not match order currency ({Currency})");

            var subtotal = GetSubtotal();
            if (discountAmount.IsGreaterThan(subtotal))
                throw new DomainException("Discount amount cannot be greater than subtotal.");

            DiscountAmount = discountAmount;
        }

        public void ApplyDiscountPercentage(decimal discountPercentage)
        {
            ValidateCanModify();

            if (discountPercentage < 0 || discountPercentage > 100)
                throw new DomainException("Discount percentage must be between 0 and 100.");

            var subtotal = GetSubtotal();
            DiscountAmount = subtotal.ApplyDiscount(discountPercentage);
        }

        public void RemoveDiscount()
        {
            ValidateCanModify();
            DiscountAmount = null;
        }

        public Money GetSubtotal()
        {
            if (!_orderItems.Any())
                return Money.Zero();

            return _orderItems.Aggregate(Money.Zero(), (sum, item) => sum + item.GetSubtotal());
        }

        public Money GetTotal()
        {
            var subtotal = GetSubtotal();
            var total = subtotal;

            if (DiscountAmount != null)
                total = total - DiscountAmount;

            if (TaxAmount != null)
                total = total + TaxAmount;

            if (ShippingFee != null)
                total = total + ShippingFee;

            return total;
        }

        public Money GetTotalDiscount()
        {
            var itemDiscounts = _orderItems.Sum(item => item.GetTotalDiscount().Amount);
            var orderDiscount = DiscountAmount?.Amount ?? 0m;
            return Money.Create(itemDiscounts + orderDiscount);
        }

        public Money GetTotalTax()
        {
            return TaxAmount ?? Money.Zero();
        }

        public Money GetTotalShipping()
        {
            return ShippingFee ?? Money.Zero();
        }

        public void CompleteOrder()
        {
            if (Status != OrderStatus.Pending)
                throw new DomainException("Only pending orders can be completed.");

            Status = OrderStatus.Completed;
            CompletedDate = DateTime.UtcNow;
        }

        public void CancelOrder(string reason)
        {
            if (Status == OrderStatus.Completed)
                throw new DomainException("Completed orders cannot be cancelled.");

            Status = OrderStatus.Cancelled;
            CancelledDate = DateTime.UtcNow;
            CancellationReason = reason;
        }
    }
}

    