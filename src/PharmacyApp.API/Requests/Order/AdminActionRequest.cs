using PharmacyApp.API.Requests.Order;

namespace PharmacyApp.API.Requests.Order
{
    public record AdminActionRequest(
        Guid AdminId
    );
}