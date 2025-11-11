using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.RemoveItem
{
    public record RemoveItemFromCartCommand(
   Guid CustomerId,
     Guid CartItemId
    ):IRequest<CartDto>;
}