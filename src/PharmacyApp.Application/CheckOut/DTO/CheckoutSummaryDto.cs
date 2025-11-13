

namespace PharmacyApp.Application.CheckOut.DTO
{
    public record CheckoutSummaryDto(
            Guid CheckoutId,
            int ItemsCount,
            decimal SubTotal,
            decimal ShippingCost,
            decimal Tax,
            decimal Discount,
            decimal TotalAmount,
            string Currency
        );
}