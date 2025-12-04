using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;
using PharmacyApp.Application.Product.DTO;

namespace PharmacyApp.Application.Product.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product with ID {request.ProductId} not found");

            await _productRepository.RemoveAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}