using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Enums;
using PharmacyApp.Domain.CartManagement.Services;
using CartEntity = PharmacyApp.Domain.CartManagement.Entities.Cart;

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
            var subtotal = cart.Items.Sum(i => i.GetSubtotal().Amount);
            var discount = cart.Items.Sum(i => i.Discount?.Amount ?? 0);
            var tax = CalculateTax(subtotal);

            return subtotal - discount + tax;
        }
    }
}
