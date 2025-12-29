using MediatR;
using PharmacyApp.Application.Common;
using PharmacyApp.Application.CartManagement.Command.RemoveItem;
using PharmacyApp.Domain.CartManagement.Repositories;

namespace PharmacyApp.Application.CartManagement.Command.RemoveItem;

public class RemoveItemFromCartCommandHandler
    : IRequestHandler<RemoveItemFromCartCommand, bool>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveItemFromCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        RemoveItemFromCartCommand request,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository
            .GetActiveCartByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart == null)
            return false;

        var removed = cart.RemoveItem(request.CartItemId);
        if (!removed)
            return false;
        _cartRepository.Update(cart);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
