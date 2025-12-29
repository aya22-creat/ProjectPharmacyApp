using PharmacyApp.Application.Common;
using PharmacyApp.Application.CartManagement.DTO;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Application.CartManagement.Command.AddItem;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Services;
using PharmacyApp.Domain.CartManagement.Enum;

public class AddItemsToCartCommandHandler : BaseCommandHandler<AddItemsToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartCalculationService _cartCalculationService;

    public AddItemsToCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        ICartCalculationService cartCalculationService)
        : base(unitOfWork)
    {
        _cartRepository = cartRepository;
        _cartCalculationService = cartCalculationService;
    }

   public override async Task<CartDto> Handle(AddItemsToCartCommand request, CancellationToken cancellationToken)
{
    var cart = await _cartRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

    if (cart == null || cart.State != CartStateEnum.Active)
    {
        if (cart != null)
        {
            await _cartRepository.DeleteCartAsync(cart, cancellationToken);
            await SaveChangesAsync(cancellationToken); 
        }

        cart = Cart.Create(request.CustomerId);
        await _cartRepository.AddAsync(cart, cancellationToken);
        await SaveChangesAsync(cancellationToken); 
    }

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

    await SaveChangesAsync(cancellationToken);

    return new CartDto(cart, _cartCalculationService, addedItems);
}


    }

