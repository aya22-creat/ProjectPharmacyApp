using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Order
{
    public record CreateOrderRequest(
        [Required] Guid CustomerId,
        [Required] List<CreateOrderItemRequest> Items,
        [Required] string Currency,
        [Required] string ShippingAddress,
        [Required] string BillingAddress,
        [Required] string PaymentMethod,
        decimal ShippingCost
    );

    public record CreateOrderItemRequest(
        [Required] Guid ProductId,
        [Required] string ProductName,
        [Required] int Quantity,
        [Required] decimal Price
    );
}