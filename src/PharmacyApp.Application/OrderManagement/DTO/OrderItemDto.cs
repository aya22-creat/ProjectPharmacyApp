
namespace PharmacyApp.Application.OrderManagement.DTO
{

    public record OrderItemDto(
 Guid Id,
 string Name,
 decimal Quantity,
 decimal Price,
 decimal Total,
 string Note);


}