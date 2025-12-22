using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Product;
public record CreateProductRequest(
    string ProductName,
    string Description,
    decimal Price,
    string? Currency,
    Guid CategoryId,
    int StockQuantity,
    bool IsCosmetic
);
