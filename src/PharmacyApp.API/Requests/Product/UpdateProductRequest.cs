using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Product;

public record UpdateProductRequest(
    string ProductName,
    string Description,
    decimal Price,
    string? Currency,
    Guid CategoryId,
    int StockQuantity
);