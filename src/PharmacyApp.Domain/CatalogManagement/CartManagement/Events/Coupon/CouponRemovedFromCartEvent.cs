using PharmacyApp.Common.Common.DomainEvent;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Events.Coupon
{
    public class CouponRemovedFromCartEvent : DomainEvent
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }

        public CouponRemovedFromCartEvent(Guid cartId, Guid customerId)
        {
            CartId = cartId;
            CustomerId = customerId;
        }
    }
}
