using MediatR;

namespace PharmacyApp.Application.CartManagement.Command.ClearCart
{
    public record ClearCartCommand(
         Guid CustomerId
    ) : IRequest<Unit>; // Unit indicates no return value; just signals completion
}
