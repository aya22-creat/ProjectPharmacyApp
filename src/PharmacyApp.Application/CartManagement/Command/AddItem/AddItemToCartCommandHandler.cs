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

        foreach (var item in request.Items.DistinctBy(x => x.ProductId))
        {
            var price = Money.Create(item.Price, "EGP");
            cart.AddItem(item.ProductId, item.ProductName, item.Quantity, price);
        }

        await SaveChangesAsync(cancellationToken);

        var cartItemsDto = cart.Items.Select(i => new CartItemDto(i)).ToList();

        var totalItems = cart.GetTotalItemsCount();
        var subTotal = cart.Items.Sum(i => i.GetSubtotal().Amount);
        var totalAmount = _cartCalculationService.CalculateTotal(cart);
        var currency = cart.GetTotal().Currency ?? "EGP";

        return new CartDto(
            cart.Id,
            cart.CustomerId,
            cartItemsDto,
            totalItems,
            subTotal,
            totalAmount,
            currency,
            cart.CreatedAt,
            cart.UpdatedAt
        );
    }
}
