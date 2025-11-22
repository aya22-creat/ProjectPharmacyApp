using System.IO.IsolatedStorage;
using PharmacyApp.Common.Common.DomainEvent; 
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;
using MediatR;


namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
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