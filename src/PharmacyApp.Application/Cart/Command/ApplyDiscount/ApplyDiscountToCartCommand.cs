using MediatR;
using PharmacyApp.Application.Cart.DTO;

namespace PharmacyApp.Application.Cart.Command.ApplyDiscount{
    public record ApplyDiscountToCartCommand(
         Guid CustomerId,
         string CouponCode
    ):IRequest<CartDto>;
}