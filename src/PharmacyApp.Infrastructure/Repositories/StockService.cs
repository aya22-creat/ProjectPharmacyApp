using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ReserveStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {productId} not found.");
            product.UpdateStock(-quantity);
            await _productRepository.UpdateAsync(product);
        }

        public async Task ReleaseStockAsync(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {productId} not found.");
            product.UpdateStock(quantity);
            await _productRepository.UpdateAsync(product);
        }
    }
}
