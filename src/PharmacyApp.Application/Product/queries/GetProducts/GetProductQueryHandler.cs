using MediatR;
using PharmacyApp.Application.Product.DTO;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;

namespace PharmacyApp.Application.Product.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            return products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description.Value,
                p.Price.Value,
                p.Stock
            ));
        }
    }
}
