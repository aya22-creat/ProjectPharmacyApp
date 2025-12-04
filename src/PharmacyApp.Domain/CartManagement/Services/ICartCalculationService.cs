using PharmacyApp.Domain.CartManagement.Enum;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public interface ICartCalculationService
    {
        decimal CalculateTax(decimal subtotal);
        decimal CalculateDiscount(decimal subtotal, DiscountType type, decimal value);
        decimal CalculateTotal(Cart cart);
    }
}
