using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Cart
{
    public record CheckoutCartRequest(
    [Required] Guid CustomerId,
    [Required] string ShippingAddress,
    [Required] string BillingAddress,
    [Required] string PaymentMethod
);
}