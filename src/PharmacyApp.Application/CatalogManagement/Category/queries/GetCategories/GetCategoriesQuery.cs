using MediatR;

using PharmacyApp.Application.CatalogManagement.Category.DTO;

namespace PharmacyApp.Application.CatalogManagement.Category.queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

}