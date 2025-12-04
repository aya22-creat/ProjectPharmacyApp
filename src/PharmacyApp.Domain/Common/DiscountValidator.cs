using PharmacyApp.Common.Common.Exception;
using CartMoney = PharmacyApp.Domain.CartManagement.ValueObjects.Money;
using OrderMoney = PharmacyApp.Domain.OrderManagement.ValueObjects.Money;

namespace PharmacyApp.Domain.Common
{
    public static class DiscountValidator
    {
        public static void ValidateCartDiscount(CartMoney discount, CartMoney subtotal)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            if (discount.IsNegative())
                throw new DomainException("Discount cannot be negative");

            if (discount.IsGreaterThan(subtotal))
                throw new DomainException("Discount cannot exceed subtotal");
        }

        public static void ValidateOrderDiscount(OrderMoney discount, OrderMoney subtotal)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            if (discount.IsNegative())
                throw new DomainException("Discount cannot be negative");

            if (discount.IsGreaterThan(subtotal))
                throw new DomainException("Discount cannot exceed subtotal");
        }
    }
}
