using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Command.AddItem
{
    public record AddItemsToCartCommand(
        Guid CustomerId,
        List<AddToCartItemDto> Items
    ) : IRequest<CartDto>;
}
