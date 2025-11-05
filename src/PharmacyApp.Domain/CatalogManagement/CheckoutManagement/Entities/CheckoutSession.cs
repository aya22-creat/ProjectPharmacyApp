using System;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Shared.ValueObjects;


namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities
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

        private CheckoutAggregate() { }

        private CheckoutAggregate(
            Guid customerId,
            Guid cartId,
            Address shippingAddress,
            Address billingAddress,
            PaymentMethod paymentMethod,
            Money totalPrice)
        {
            if (cartId == Guid.Empty)
                throw new DomainException("Cart ID cannot be empty.");

            if (totalPrice.Amount <= 0)
                throw new DomainException("Total price must be greater than zero.");

            CustomerId = customerId;
            CartId = cartId;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            PaymentMethod = paymentMethod;
            TotalPrice = totalPrice;
            Status = CheckoutStatus.Pending;
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
        }

        public void CancelCheckout()
        {
            if (Status == CheckoutStatus.Completed)
                throw new DomainException("Cannot cancel a completed checkout.");

            Status = CheckoutStatus.Canceled;
        }
    }

   
}
