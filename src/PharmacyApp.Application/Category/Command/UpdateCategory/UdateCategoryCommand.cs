using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Category.DTO;


namespace PharmacyApp.Application.Category.Command.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id ,string Name ,string Description) : IRequest<CategoryDto>;
}
