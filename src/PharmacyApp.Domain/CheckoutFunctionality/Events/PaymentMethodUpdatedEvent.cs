using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CheckoutFunctionality.ValueObjects;
using MediatR;
namespace PharmacyApp.Domain.CheckoutFunctionality.Events
{
    public class PaymentMethodUpdatedEvent : DomainEvent, INotification
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
