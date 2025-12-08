using System;
using MediatR;
using PharmacyApp.Application.Product.DTO;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Application.Product.Queries.GetProductById
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            return product == null ? null : new ProductDto(
                product.Id,
                product.Name,
                product.Description.Value,
                product.Price.Amount,
                product.StockQuantity,
                product.CategoryId,
                product.CreatedAt,
                product.UpdatedAt,
                product.IsCosmetic,
                product.IsAvailable
            );
        }
    }
}
