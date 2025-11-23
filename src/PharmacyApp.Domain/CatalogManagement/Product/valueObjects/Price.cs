using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects
{

    public class Price : ValueObject
    {
        public decimal Value { get; private set; }
        public string Currency { get; private set; } = "EGP";

       

        private Price(decimal value)
        {
            if (value < 0)
                throw new DomainException("Price value cannot be negative.");

            Value = Math.Round(value, 2);
        }



        public static Price Create(decimal value)
        {
            return new Price(value);
        }

        public static Price Zero() => new Price(0);



        public Price Add(Price other)
        {
            EnsureNotNull(other);
            ValidateSameCurrency(other);
            return new Price(Value + other.Value);
        }

        public Price Subtract(Price other)
        {
            EnsureNotNull(other);
            ValidateSameCurrency(other);

            if (other.Value > Value)
                throw new DomainException("Cannot subtract a price greater than the current value.");

            return new Price(Value - other.Value);
        }

        public Price Multiply(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Quantity cannot be negative.");

            return new Price(Value * quantity);
        }

        public Price ApplyDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new DomainException("Discount percentage must be between 0 and 100.");

            var discount = Value * (percentage / 100);
            return new Price(Value - discount);
        }


        public static Price operator +(Price a, Price b)
        {
            EnsureNotNull(a);
            EnsureNotNull(b);
            a.ValidateSameCurrency(b);
            return a.Add(b);
        }

        public static Price operator -(Price a, Price b)
        {
            EnsureNotNull(a);
            EnsureNotNull(b);
            a.ValidateSameCurrency(b);
            return a.Subtract(b);
        }

        public static Price operator *(Price a, int quantity)
        {
            EnsureNotNull(a);
            return a.Multiply(quantity);
        }



        private static void EnsureNotNull(Price? price)
        {
            if (price == null)
                throw new ArgumentNullException(nameof(price), "Price instance cannot be null.");
        }

        private void ValidateSameCurrency(Price other)
        {
            if (Currency != other.Currency)
                throw new DomainException($"Cannot operate on prices with different currencies: {Currency} vs {other.Currency}");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }



        public override string ToString() => $"{Value:N2} {Currency}";

    }
}
