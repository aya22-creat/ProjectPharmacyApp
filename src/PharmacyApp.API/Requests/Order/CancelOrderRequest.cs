using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Order
{
    public record CancelOrderRequest(
        [Required] Guid CustomerId,
        string? Reason
    );
}