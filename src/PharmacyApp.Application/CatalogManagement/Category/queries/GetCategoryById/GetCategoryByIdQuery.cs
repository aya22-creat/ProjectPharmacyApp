using MediatR;

using PharmacyApp.Application.CatalogManagement.Category.DTO;

namespace PharmacyApp.Application.CatalogManagement.Category.queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;

}