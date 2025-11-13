using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Command.CompleteCheckout
{
    public record CompleteCheckoutCommand(
        Guid CheckoutId
    ) : IRequest<CheckoutResultDto>;
}