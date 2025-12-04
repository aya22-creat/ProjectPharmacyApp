
namespace PharmacyApp.Application.Order.DTO
{

    public record OrderItemDto(
 Guid Id,
 string Name,
 decimal Quantity,
 decimal Price,
 decimal Tax,
 decimal Total,
 string Note);


}
