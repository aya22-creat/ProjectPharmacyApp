using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.CheckoutCart
{
    public record CheckoutCartCommand(
Guid CustomerId,
string ShippingAddress,
string BillingAddress,
string PaymentMethod

    ):IRequest<Guid>; // ID Oder
}
