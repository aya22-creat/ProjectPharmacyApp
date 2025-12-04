using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Services;
using CartEntity = PharmacyApp.Domain.CartManagement.Cart;


namespace PharmacyApp.Application.Common
{
    public class CartCalculationService : ICartCalculationService
    {
        public decimal CalculateTax(decimal subtotal)
        {
            return subtotal * Constants.TaxRate;
        }

        public decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value)
        {
            return type switch
            {
                DiscountType.Percentage => subtotal * (value / 100),
                DiscountType.FixedAmount => value,
                _ => 0
            };
        }

        public decimal CalculateTotal(CartEntity cart)
        {
            var subtotal = cart.Items.Sum(static i => i.GetSubtotal().Amount);
            var discount = cart.Discount?.Amount ?? 0;
            var tax = CalculateTax(subtotal);

            return subtotal - discount + tax;
        }
    }
}
