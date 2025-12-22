using PharmacyApp.API.Requests.Order;

namespace PharmacyApp.API.Requests.Category
{
    public record CreateCategoryRequest(
        string Name,
        string Description
    );
}