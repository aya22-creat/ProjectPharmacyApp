using PharmacyApp.Domain.CartManagement.Entities;
using PharmacyApp.Domain.CartManagement.Enums;
using CartEntity = PharmacyApp.Domain.CartManagement.Entities.Cart;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public interface ICartCalculationService
    {
        decimal CalculateTax(decimal subtotal);
        decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value);
        decimal CalculateTotal(CartEntity cart);
    }
}
