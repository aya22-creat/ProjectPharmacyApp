

namespace PharmacyApp.Application.CatalogManagement.Category.DTO

{
    public record CategoryDto(Guid Id, string? Name, int ProductCount, string? Description);
}