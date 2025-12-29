using MediatR;

namespace PharmacyApp.Application.CartManagement.Command.DeleteCart
{
    public record DeleteCartCommand(
         Guid CustomerId
    ) : IRequest<Unit>; // Unit indicates no return value; just signals completion
}
