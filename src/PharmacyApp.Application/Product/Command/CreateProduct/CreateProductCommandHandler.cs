using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Product.DTO;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.Repositories;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CategoryManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.ProductManagement.AggregateRoots;

namespace PharmacyApp.Application.Product.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

     public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var price = Price.Create(request.Price);
            var categoryId = CategoryId.Create(request.CategoryId);
            var description = new ProductDescription(request.Description);

            var product = ProductAggregate.Create(request.Name, description, price, request.StockQuantity, categoryId);

            await _productRepository.AddAsync(product, cancellationToken);
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
