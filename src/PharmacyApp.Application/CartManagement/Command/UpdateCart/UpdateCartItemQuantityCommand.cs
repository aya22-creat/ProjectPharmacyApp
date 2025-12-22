using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Command.UpdateCart{
     public record UpdateCartItemQuantityCommand(

        Guid CustomerId,
        Guid CartItemId,
        int NewQuantity
    ):IRequest<CartDto>;


}