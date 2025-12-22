using System;
using MediatR;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Application.CatalogManagement.Product.DTO;


namespace PharmacyApp.Application.CatalogManagement.Product.Command.CreateProduct
{
    public record CreateProductCommand(
        string ProductName,
        string Description,
        decimal Price,
        string Currency,
        Guid CategoryId,
        int StockQuantity,
        bool IsCosmetic
    ) : IRequest<ProductDto>;
}
