using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.UpdateCart{
     public record UpdateCartItemQuantityCommand(

        Guid CustomerId,
        Guid CartItemId,
        int NewQuantity
    ):IRequest<CartDto>;


}