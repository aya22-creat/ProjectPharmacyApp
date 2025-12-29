using MediatR;
using PharmacyApp.Application.CartManagement.DTO;
using PharmacyApp.Application.CartManagement.Command.AddItem;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Enum;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CartManagement.Services;

public class AddItemsToCartCommandHandler : IRequestHandler<AddItemsToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartCalculationService _cartCalculationService;

    public AddItemsToCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        ICartCalculationService cartCalculationService)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
        _cartCalculationService = cartCalculationService;
    }

    public async Task<CartDto> Handle(AddItemsToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart == null)
        {
            cart = Cart.Create(request.CustomerId);
            await _cartRepository.AddAsync(cart, cancellationToken);
        }
        else if (cart.State != CartStateEnum.Active)
        {
            cart.Activate();
        }

        cart.ClearCart();
        _cartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync(cancellationToken); // commit الحذف أولًا

        var addedItems = new List<AddedCartItemDto>();

        foreach (var item in request.Items.DistinctBy(x => x.ProductId))
        {
            var price = Money.Create(item.Price, "EGP");
            cart.AddItem(item.ProductId, item.ProductName, item.Quantity, price);

            addedItems.Add(new AddedCartItemDto(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                price.Multiply(item.Quantity).Amount
            ));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CartDto(cart, _cartCalculationService, addedItems);
    }
}
