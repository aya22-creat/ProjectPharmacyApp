using PharmacyApp.Common.Common.Exception;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;


namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.services
{

    public class ProductCatalogService
    {
        private readonly IProductRepository _productRepository;

        public ProductCatalogService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> IsProductNameUniqueAsync(string name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            var exists = await _productRepository.ExistsByNameAsync(name, cancellationToken);
            return !exists;
        }

    }
}
