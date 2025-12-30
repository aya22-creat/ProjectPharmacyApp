using PharmacyApp.Application.Common;
using PharmacyApp.Application.CartManagement.DTO;
using PharmacyApp.Common.Common.ValueObjects;
using PharmacyApp.Domain.CartManagement;
using PharmacyApp.Domain.CartManagement.Repositories;
using PharmacyApp.Domain.CartManagement.Services;
using CartEntity = PharmacyApp.Domain.CartManagement.Cart;
using PharmacyApp.Domain.CatalogManagement.Product.Repositories;

namespace PharmacyApp.Application.CartManagement.Command.AddItem;

public class AddItemsToCartCommandHandler
    : BaseCommandHandler<AddItemsToCartCommand, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICartCalculationService _cartCalculationService;

    public AddItemsToCartCommandHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        ICartCalculationService cartCalculationService)
        : base(unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _cartCalculationService = cartCalculationService;
    }

    public override async Task<CartDto> Handle(
        AddItemsToCartCommand request,
        CancellationToken cancellationToken)
    {
        if (request.CustomerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required");

        if (request.Items == null || !request.Items.Any())
            throw new ArgumentException("Items list cannot be empty");

        var cart = await _cartRepository.GetActiveCartByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (cart == null)
        {
            cart = CartEntity.Create(request.CustomerId);
            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        foreach (var item in request.Items)
        {
            if (item.ProductId == Guid.Empty)
                throw new ArgumentException($"Invalid ProductId for item {item.ProductName}");

            if (item.Quantity <= 0)
                throw new ArgumentException($"Quantity must be greater than 0 for {item.ProductName}");

            if (item.Price <= 0)
                throw new ArgumentException($"Price must be greater than 0 for {item.ProductName}");

            var exists = await _productRepository.ExistsByIdAsync(item.ProductId, cancellationToken);
            if (!exists)
                throw new KeyNotFoundException($"Product {item.ProductName} with ID {item.ProductId} does not exist.");

            var money = Money.Create(item.Price, item.Currency ?? Constants.DefaultCurrency);

            cart.AddItem(
                productId: item.ProductId,
                productName: item.ProductName,
                quantity: item.Quantity,
                price: money,
                currency: item.Currency
            );
        }


        await SaveChangesAsync(cancellationToken);

        var subTotal = cart.Items.Sum(i => i.GetSubtotal().Amount);
        var total = _cartCalculationService.CalculateTotal(cart);

        return new CartDto(
            Id: cart.Id,
            CustomerId: cart.CustomerId,
            Items: cart.Items.Select(i => new CartItemDto(i)).ToList(),
            TotalItems: cart.GetTotalItemsCount(),
            SubTotal: subTotal,
            TotalAmount: total,
            Currency: cart.GetTotal().Currency ?? Constants.DefaultCurrency,
            CreatedAt: cart.CreatedAt,
            UpdatedAt: cart.UpdatedAt
        );
    }
}
