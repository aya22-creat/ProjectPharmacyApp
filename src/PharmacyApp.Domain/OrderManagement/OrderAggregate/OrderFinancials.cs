using System;
using PharmacyApp.Domain.OrderManagement.ValueObjects;



namespace PharmacyApp.Domain.OrderManagement.OrderAggregate;

public partial class Order
{
    public void ApplyDiscount(Money discount)
    {
        if (discount.Amount < 0) throw new ArgumentException("Discount cannot be negative");
        if (discount.Amount > SubTotal.Amount) throw new ArgumentException("Discount cannot exceed subtotal");

        Discount = discount;
    }

    public void ApplyTax(Money tax)
    {
        if (tax.Amount < 0) throw new ArgumentException("Tax cannot be negative");
        Tax = tax;
    }

    public void UpdateShippingCost(Money shippingCost)
    {
        if (shippingCost.Amount < 0) throw new ArgumentException("Shipping cost cannot be negative");
        ShippingCost = shippingCost;
    }
}
