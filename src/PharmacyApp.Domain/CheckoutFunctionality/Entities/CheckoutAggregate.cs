using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CartManagement.ValueObjects;
using PharmacyApp.Domain.CheckoutFunctionality.ValueObjects;
using PharmacyApp.Domain.CheckoutFunctionality.Events;

namespace PharmacyApp.Domain.CheckoutFunctionality.Entities
{
    public class CheckoutAggregate : AggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }
        public Guid CartId { get; private set; }
        public Address ShippingAddress { get; private set; } = null!;
        public Address BillingAddress { get; private set; } = null!;
        public PaymentMethod PaymentMethod { get; private set; } = null!;
        public Money TotalPrice { get; private set; } = null!;
        public CheckoutStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancellationReason { get; private set; }



        private CheckoutAggregate() { }

        private CheckoutAggregate(
            Guid customerId,
            Guid cartId,
            Address shippingAddress,
            Address billingAddress,
            PaymentMethod paymentMethod,
            Money totalPrice)
        {
            if (customerId == Guid.Empty)
                throw new DomainException("Customer ID cannot be empty.");

            if (cartId == Guid.Empty)
                throw new DomainException("Cart ID cannot be empty.");

            if (totalPrice == null || totalPrice.Amount <= 0)
                throw new DomainException("Total price must be greater than zero.");

            CustomerId = customerId;
            CartId = cartId;
            ShippingAddress = shippingAddress ?? throw new DomainException("Shipping address is required.");
            BillingAddress = billingAddress ?? throw new DomainException("Billing address is required.");
            PaymentMethod = paymentMethod ?? throw new DomainException("Payment method is required.");
            TotalPrice = totalPrice;
            Status = CheckoutStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public static CheckoutAggregate Create(
            Guid customerId,
            Guid cartId,
            Address shippingAddress,
            Address billingAddress,
            PaymentMethod paymentMethod,
            Money totalPrice)
        {
            return new CheckoutAggregate(customerId, cartId, shippingAddress, billingAddress, paymentMethod, totalPrice);
        }

       public void CompleteCheckout()
        {
            if (Status != CheckoutStatus.Pending)
                throw new DomainException("Checkout already completed or canceled.");

            Status = CheckoutStatus.Completed;
            CompletedAt = DateTime.UtcNow;

            AddDomainEvent(new CheckoutCompletedEvent(Id, CustomerId, CartId, CompletedAt.Value, TotalPrice.Amount));
        }

        public void CancelCheckout()
        {
            if (Status == CheckoutStatus.Completed)
                throw new DomainException("Cannot cancel a completed checkout.");

            Status = CheckoutStatus.Canceled;
        }
    }
}
