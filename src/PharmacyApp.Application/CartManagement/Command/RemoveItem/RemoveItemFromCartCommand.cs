using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Command.RemoveItem
{
    public record RemoveItemFromCartCommand(
   Guid CustomerId,
     Guid CartItemId
    ):IRequest<CartDto>;
}