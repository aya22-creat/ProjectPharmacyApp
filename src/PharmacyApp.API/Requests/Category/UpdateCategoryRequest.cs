using PharmacyApp.API.Requests.Order;

namespace PharmacyApp.API.Requests.Category
{
    public record UpdateCategoryRequest(string Name, string Description);
}