using PharmacyApp.Application.Common;
using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Domain.CartManagement.Services;
using CartEntity = PharmacyApp.Domain.CartManagement.Cart;


namespace PharmacyApp.Application.Common
{
    public class CartCalculationService : ICartCalculationService
    {
        public decimal CalculateTotal(CartEntity cart)
        {
            var subtotal = cart.Items.Sum(static i => i.GetSubtotal().Amount);

            return subtotal ;
        }
    }
}
