using System;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Application.Product.DTO;


namespace PharmacyApp.Application.Product.Command.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal Price,
        string Currency,
        Guid CategoryId,
        int StockQuantity
    ) : IRequest<ProductDto>;
}
