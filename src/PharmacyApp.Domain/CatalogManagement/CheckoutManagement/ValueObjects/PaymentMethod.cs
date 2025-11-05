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
