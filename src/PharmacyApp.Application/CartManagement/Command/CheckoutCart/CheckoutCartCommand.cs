using MediatR;
using PharmacyApp.Application.CartManagement.DTO;

namespace PharmacyApp.Application.CartManagement.Command.CheckoutCart
{
    public record CheckoutCartCommand(
Guid CustomerId,
string ShippingAddress,
string BillingAddress,
string PaymentMethod

    ):IRequest<Guid>; // ID Oder
}
