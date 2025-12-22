using System;

namespace PharmacyApp.Application.CatalogManagement.Product.DTO
{

    public record ProductDto(
        Guid Id,
        string ProductName,
        string Description,
        decimal Price,
        string Currency,
        int StockQuantity,
        Guid CategoryId,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        bool IsCosmetic,
        bool IsAvailable

    );

}
