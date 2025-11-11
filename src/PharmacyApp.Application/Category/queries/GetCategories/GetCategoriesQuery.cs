using MediatR;

using PharmacyApp.Application.Category.DTO;

namespace PharmacyApp.Application.Category.queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

}