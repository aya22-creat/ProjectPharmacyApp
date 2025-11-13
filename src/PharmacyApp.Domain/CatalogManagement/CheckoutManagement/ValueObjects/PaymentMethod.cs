using PharmacyApp.Common.Common;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects
{
    public class PaymentMethod : ValueObject
    {
        public string Type { get; private set; } = null!;
        public string Details { get; private set; } = null!;

        private PaymentMethod() { }

        public PaymentMethod(string type, string details)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Payment type is required.", nameof(type));

            if (string.IsNullOrWhiteSpace(details))
                throw new ArgumentException("Payment details are required.", nameof(details));

            Type = type;
            Details = details;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Details;
        }
    }
}
