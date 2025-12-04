using MediatR;
using PharmacyApp.Application.Product.DTO;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.Product.AggregateRoots;


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

            var product = ProductAggregate.Create(request.ProductName, description, price, request.StockQuantity, request.IsCosmetic, categoryId);

            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductDto(
                product.Id,
                product.Name,
                product.Description.Value,
                product.Price.Value,
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
