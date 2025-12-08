using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Entities;
using System.Linq;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public class CartCalculationService : ICartCalculationService
    {
        private readonly decimal _taxRate = 0.14m; 

        public decimal CalculateTax(decimal subtotal)
        {
            return subtotal * _taxRate;
        }

        public decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value)
        {
            return type switch
            {
                DiscountType.Percentage => subtotal * value / 100m,
                DiscountType.FixedAmount => value,
                _ => 0m
            };
        }

        public decimal CalculateTotal(Cart cart)
        {
            var subtotal = cart.Items.Sum(i => i.GetSubtotal().Amount);
            return subtotal ;
        }
    }
}
