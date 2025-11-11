
using MediatR;

namespace PharmacyApp.Application.Category.Command.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;
}