
using MediatR;

namespace PharmacyApp.Application.CatalogManagement.Category.Command.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
}