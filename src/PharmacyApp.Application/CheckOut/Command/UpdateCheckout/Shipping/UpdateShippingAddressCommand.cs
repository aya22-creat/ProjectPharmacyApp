using MediatR;
using PharmacyApp.Application.CheckOut.DTO;


namespace PharmacyApp.Application.CheckOut.Command.UpdateCheckout.Shipping
{
    public record UpdateShippingAddressCommand(Guid CheckoutId,AddressDto NewShippingAddress) : IRequest<CheckoutDto>;
}