using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.ClearCart
{
    public record ClearCartCommand(
         Guid CustomerId
    ) : IRequest<Unit>; // Unit indicates no return value; just signals completion
}
