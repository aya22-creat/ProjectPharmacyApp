using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.AddItem
{
    public record AddItemToCartCommand(
        Guid CustomerId,
        Guid ProductId,
        string ProductName,
        decimal Price,
        int Quantity,
        string Currency
    ) : IRequest<CartDto>;
}
