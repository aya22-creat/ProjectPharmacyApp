using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Application.Product.DTO;

namespace PharmacyApp.Application.Product.Command.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string ProductName,
        string Description,
        decimal Price,
        string Currency,
        Guid CategoryId,
        int StockQuantity
    ) : IRequest<ProductDto>;
}
