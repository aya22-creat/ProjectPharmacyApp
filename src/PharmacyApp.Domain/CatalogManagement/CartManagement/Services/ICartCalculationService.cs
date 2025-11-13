using PharmacyApp.Domain.CatalogManagement.CartManagement.Entities;
using PharmacyApp.Domain.CatalogManagement.CartManagement.Enums;
using CartEntity = PharmacyApp.Domain.CatalogManagement.CartManagement.Entities.Cart;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.Services
{
    public interface ICartCalculationService
    {
        decimal CalculateTax(decimal subtotal);
        decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value);
        decimal CalculateTotal(CartEntity cart);
    }
}
