using System.IO.IsolatedStorage;
using PharmacyApp.Common.Common.DomainEvent; 
using PharmacyApp.Domain.CheckoutFunctionality.ValueObjects;
using MediatR;


namespace PharmacyApp.Domain.CheckoutFunctionality.Events
{
    public class BillingAddressUpdatedEvent : DomainEvent ,INotification
    {
       public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public Address NewAddress { get; }

        public BillingAddressUpdatedEvent(Guid checkoutId, Guid customerId, Address newAddress)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            NewAddress = newAddress;
        }
    }
}