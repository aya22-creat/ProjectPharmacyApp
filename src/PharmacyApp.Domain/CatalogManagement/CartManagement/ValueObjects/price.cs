using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Domain.CatalogManagement.CartManagement.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string? Currency { get; private set; }

        private Money() { } // For EF Core

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required", nameof(currency));

            if (currency.Length != 3)
                throw new ArgumentException("Currency must be 3 characters (ISO 4217)", nameof(currency));

            Amount = amount;
            Currency = currency.ToUpper();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency!;
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException($"Cannot add money with different currencies: {Currency} and {other.Currency}");

            return new Money(Amount + other.Amount, Currency!);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException($"Cannot subtract money with different currencies: {Currency} and {other.Currency}");

            if (Amount < other.Amount)
                throw new InvalidOperationException("Result cannot be negative");

            return new Money(Amount - other.Amount, Currency!);
        }

        public Money Multiply(decimal factor)
        {
            if (factor < 0)
                throw new ArgumentException("Factor cannot be negative", nameof(factor));

            return new Money(Amount * factor, Currency!);
        }

        public static Money Zero(string currency) => new Money(0, currency);

        public override string ToString() => $"{Amount:F2} {Currency}";
    }
}
