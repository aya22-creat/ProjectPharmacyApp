using MediatR;
using PharmacyApp.Application.CatalogManagement.Category.DTO;


namespace PharmacyApp.Application.CatalogManagement.Category.Command.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, string Description) : IRequest<CategoryDto>;
}
