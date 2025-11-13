using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Command.CreatCheckout
{
    public record CreateCheckoutCommand(
        Guid CartId,
        Guid CustomerId,
        string ShippingAddress,
        string BillingAddress,
        string PaymentMethod,
        string Currency
    ) : IRequest<CheckoutDto>;
}