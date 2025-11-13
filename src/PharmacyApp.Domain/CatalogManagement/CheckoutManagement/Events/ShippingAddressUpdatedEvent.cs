using PharmacyApp.Common.Common.DomainEvent;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;



namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Events
{
    public class ShippingAddressUpdatedEvent : DomainEvent
    {
        public Guid CheckoutId { get; }
        public Guid CustomerId { get; }
        public Address NewAddress { get; }

        public ShippingAddressUpdatedEvent(Guid checkoutId, Guid customerId, Address newAddress)
        {
            CheckoutId = checkoutId;
            CustomerId = customerId;
            NewAddress = newAddress;
        }
    }
}
