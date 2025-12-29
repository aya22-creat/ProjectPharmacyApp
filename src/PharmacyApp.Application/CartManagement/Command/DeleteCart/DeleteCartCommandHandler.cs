using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.CartManagement.Command.DeleteCart
{
    public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart != null)
            {
                await _cartRepository.DeleteCartAsync(cart, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
