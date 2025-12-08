using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;

namespace PharmacyApp.Common.Common.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = null!;

        private Money() { }

        private Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new DomainException("Money amount cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainException("Currency cannot be null or empty.");

            Amount = Math.Round(amount, 2);
            Currency = currency.ToUpper();
        }


        public static Money Create(decimal amount, string currency = "EGP")
            => new Money(amount, currency);

        public static Money Zero(string currency = "EGP")
            => new Money(0, currency);


        public Money Add(Money other)
        {
            EnsureCompatibleCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureCompatibleCurrency(other);

            if (other.Amount > Amount)
                throw new DomainException("Cannot subtract more than the available amount.");

            return new Money(Amount - other.Amount, Currency);
        }

        public Money Multiply(decimal multiplier)
        {
            if (multiplier < 0)
                throw new DomainException("Multiplier cannot be negative.");

            return new Money(Amount * multiplier, Currency);
        }


        public bool IsZero() => Amount == 0;
        public bool IsPositive() => Amount > 0;
        public bool IsNegative() => Amount < 0;

        public bool IsGreaterThan(Money other)
        {
            EnsureCompatibleCurrency(other);
            return Amount > other.Amount;
        }

        public bool IsLessThan(Money other)
        {
            EnsureCompatibleCurrency(other);
            return Amount < other.Amount;
        }

        public bool IsEqualTo(Money other)
        {
            EnsureCompatibleCurrency(other);
            return Amount == other.Amount;
        }

        public static Money operator +(Money left, Money right)
        {
            EnsureCompatibleCurrency(left, right);
            return left.Add(right);
        }

        public static Money operator -(Money left, Money right)
        {
            EnsureCompatibleCurrency(left, right);
            return left.Subtract(right);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            EnsureNotNull(money);
            return money.Multiply(multiplier);
        }



        private static void EnsureNotNull(Money? money)
        {
            if (money == null)
                throw new ArgumentNullException(nameof(money), "Money instance cannot be null.");
        }

        private void EnsureCompatibleCurrency(Money other)
        {
            EnsureNotNull(other);
            if (!Currency.Equals(other.Currency, StringComparison.OrdinalIgnoreCase))
                throw new DomainException("Cannot operate on amounts with different currencies.");
        }

        private static void EnsureCompatibleCurrency(Money left, Money right)
        {
            EnsureNotNull(left);
            EnsureNotNull(right);
            if (!left.Currency.Equals(right.Currency, StringComparison.OrdinalIgnoreCase))
                throw new DomainException("Cannot operate on amounts with different currencies.");
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString() => $"{Amount:N2} {Currency}";
        public string ToInvoiceFormat() => $"{Currency} {Amount:F2}";
    }
}
