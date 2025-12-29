using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.CartManagement.Command.ClearCart
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

            if (cart == null)
            {
                cart = Cart.Create(request.CustomerId);
                await _cartRepository.AddAsync(cart, cancellationToken);
            }
            else if (!cart.IsEmpty())
            {
                foreach (var item in cart.Items.ToList())
                {
                    cart.RemoveItem(item.Id);

                    _cartRepository.Update(cart);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            return Unit.Value;
        }
    }
}
