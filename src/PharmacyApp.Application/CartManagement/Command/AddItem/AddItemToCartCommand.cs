using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Command.AddItem
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
