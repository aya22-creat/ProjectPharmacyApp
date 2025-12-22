using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Order
{
    public record RejectOrderRequest(
        [Required] Guid AdminId,
        string? Reason
    );
}