using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Category.DTO;

namespace PharmacyApp.Application.Category.Command.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id ) : IRequest<Unit>;
}