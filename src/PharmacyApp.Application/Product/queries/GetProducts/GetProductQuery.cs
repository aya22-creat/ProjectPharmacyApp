using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Product.DTO;

namespace PharmacyApp.Application.Product.Queries.GetProducts
{
    public record GetProductQuery : IRequest<IEnumerable<ProductDto>>;
}
