using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.CatalogManagement.Category.DTO;

namespace PharmacyApp.Application.CatalogManagement.Category.Command.CreateCategory
{
    public record CreateCategoryCommand(string Name, string Description) : IRequest<CategoryDto>;
}
