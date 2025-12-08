using PharmacyApp.Domain.CartManagement.Enum;

namespace PharmacyApp.Domain.CartManagement.Services
{
    public interface ICartCalculationService
    {
        decimal CalculateTotal(Cart cart);
    }
}
