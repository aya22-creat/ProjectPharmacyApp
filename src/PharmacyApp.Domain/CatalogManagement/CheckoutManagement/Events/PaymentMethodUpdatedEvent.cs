using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
{
    public class PaymentMethodUpdatedEvent : DomainEvent
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public PaymentMethod NewPaymentMethod { get; }

        public PaymentMethodUpdatedEvent(Guid checkoutId, Guid customerId, PaymentMethod newPaymentMethod)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            NewPaymentMethod = newPaymentMethod;
        }
    }
}
