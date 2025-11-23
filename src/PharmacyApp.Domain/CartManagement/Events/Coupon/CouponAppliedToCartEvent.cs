using MediatR;
using PharmacyApp.Common.Common.DomainEvent;


namespace PharmacyApp.Domain.CartManagement.Events.Coupon
{
    public class CouponAppliedToCartEvent : DomainEvent, INotification
    {
        public Guid CartId { get; }
        public Guid CustomerId { get; }
        public string CouponCode { get; }
        public decimal DiscountAmount { get; }

        public CouponAppliedToCartEvent(
            Guid cartId,
            Guid customerId,
            string couponCode,
            decimal discountAmount)
        {
            CartId = cartId;
            CustomerId = customerId;
            CouponCode = couponCode;
            DiscountAmount = discountAmount;
        }
    }
}
