using MediatR;
using PharmacyApp.Application.CatalogManagement.Product.DTO;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;

namespace PharmacyApp.Application.CatalogManagement.Product.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {request.Id} not found");

            var price = Money.Create(request.Price);
            var categoryId = CategoryId.Create(request.CategoryId);
            var description = new ProductDescription(request.Description);

            product.UpdateName(request.ProductName);
            product.UpdateDescription(description);
            product.UpdatePrice(price);
            product.UpdateCategory(categoryId);
            product.UpdateStock(request.StockQuantity - product.StockQuantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductDto(
                product.Id,
                product.ProductName,
                product.Description.Value,
                product.Price.Amount,
                product.Price.Currency,
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