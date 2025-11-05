using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Product.DTO;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.ValueObjects;

namespace PharmacyApp.Application.Product.Command.UpdateProduct
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

            var price = Price.Create(request.Price);
            var categoryId = CategoryId.Create(request.CategoryId);
            var description = new ProductDescription(request.Description);

            product.UpdateName(request.Name);
            product.UpdateDescription(description);
            product.UpdatePrice(price);
            product.UpdateCategory(categoryId);
            product.UpdateStock(request.StockQuantity - product.Stock);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductDto(
                product.Id,
                product.Name,
                product.Description.Value,
                product.Price.Value,
                product.Stock
            );
        }
    }
}