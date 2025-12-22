using System.ComponentModel.DataAnnotations;

namespace PharmacyApp.API.Requests.Cart

{
 public record UpdateQuantityRequest(int NewQuantity);
}