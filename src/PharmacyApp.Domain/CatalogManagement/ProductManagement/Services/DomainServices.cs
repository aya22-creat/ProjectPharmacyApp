using System;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;


namespace PharmacyApp.Domain.CatalogManagement.ProductManagement
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
            return !await _productRepository.ExistsByNameAsync(name, cancellationToken);
        }

    }
}