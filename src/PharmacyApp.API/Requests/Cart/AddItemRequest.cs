using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Cart
{
    public record AddItemRequest(
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity,
    string? Currency
    
);
}