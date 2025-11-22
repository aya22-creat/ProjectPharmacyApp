using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Command.CreateCheckout
{
    public record CreateCheckoutCommand(
        Guid CartId,
        Guid CustomerId,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentMethodDto PaymentMethod
    ) : IRequest<CheckoutDto>;
}
