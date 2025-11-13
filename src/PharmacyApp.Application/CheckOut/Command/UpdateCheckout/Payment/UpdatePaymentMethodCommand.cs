using MediatR;
using PharmacyApp.Application.CheckOut.DTO;



namespace PharmacyApp.Application.CheckOut.Command.UpdateCheckout.Payment
{
    public record UpdatePaymentMethodCommand(Guid CheckoutId,PaymentMethodDto NewPaymentMethod) : IRequest<CheckoutDto>;
}