using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects;
using PharmacyApp.Domain.CatalogManagement.CheckoutManagement.Entities;

namespace PharmacyApp.Application.CheckOut.DTO
{
    public record CheckoutDto(
            Guid Id,
        Guid CustomerId,
        Guid CartId,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentMethodDto PaymentMethod,
        decimal TotalPrice,
        string Currency,
        string Status,
        DateTime CreatedAt
        )

    
    { public CheckoutDto(CheckoutAggregate checkout) : this(
            Id: checkout.Id,
            CustomerId: checkout.CustomerId,
            CartId: checkout.CartId,
            ShippingAddress: new AddressDto(checkout.ShippingAddress),
            BillingAddress: new AddressDto(checkout.BillingAddress),
            PaymentMethod: new PaymentMethodDto(checkout.PaymentMethod),
            TotalPrice: checkout.TotalPrice.Amount,
            Currency: checkout.TotalPrice.Currency,
            Status: checkout.Status.ToString(),
            CreatedAt: checkout.CreatedAt
        ) { }
    }



        public record AddressDto(
        string Street,
        string City,
        string Country,
        string ZipCode
    )
{
     public AddressDto(Address address) : this(
            Street: address.Street,
            City: address.City,
            Country: address.Country,
            ZipCode: address.ZipCode
        ) { }
}


    public record PaymentMethodDto(
        string Type,
        string Details
    )
    {
        public PaymentMethodDto(PaymentMethod paymentMethod) : this(
            Type: paymentMethod.Type,
            Details: paymentMethod.Details
        ) { }
    }
     public record CreateCheckoutDto(
        Guid CustomerId,
        Guid CartId,
        AddressDto ShippingAddress,
        AddressDto BillingAddress,
        PaymentMethodDto PaymentMethod
    );


      public record CheckoutResultDto(
        Guid CheckoutId,
        Guid OrderId,
        string Status,
        string Message
    );
}
