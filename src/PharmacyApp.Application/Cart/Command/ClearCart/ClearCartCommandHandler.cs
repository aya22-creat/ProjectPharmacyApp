using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Application.Cart.Command;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.Cart.Command.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClearCartCommandHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (cart != null)
            {
                cart.ClearCart();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
