using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Enum;
using System.Linq;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public class CartCalculationService : ICartCalculationService
    {
        private readonly decimal _taxRate = 0.14m;

        public decimal CalculateTax(decimal subtotal) => subtotal * _taxRate;

        public decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value) =>
            type switch
            {
                DiscountType.Percentage => subtotal * value / 100m,
                DiscountType.FixedAmount => value,
                _ => 0m
            };

        public decimal CalculateTotal(Cart cart)
        {
            return cart.Items.Sum(i => i.GetSubtotal().Amount);
        }
    }
}
