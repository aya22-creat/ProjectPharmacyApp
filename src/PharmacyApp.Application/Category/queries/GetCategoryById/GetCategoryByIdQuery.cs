using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Application.Category.DTO;

namespace PharmacyApp.Application.Category.queries.GetCatwegoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;

}