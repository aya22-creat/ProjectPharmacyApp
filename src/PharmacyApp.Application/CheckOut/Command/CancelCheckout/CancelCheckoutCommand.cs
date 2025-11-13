using MediatR;
using PharmacyApp.Application.CheckOut.DTO;

namespace PharmacyApp.Application.CheckOut.Command.CancelCheckout
{
   public record CancelCheckoutCommand(
        Guid CheckoutId,
        string Reason
    ) : IRequest<Unit>;
}