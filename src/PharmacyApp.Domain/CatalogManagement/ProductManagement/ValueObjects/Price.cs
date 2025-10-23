using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.CatalogManagement.ProductManagement.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Value { get; private set; }
        public string Currency { get; init; } = string.Empty;

        private Price() { } // For ORM

        public Price(decimal value, string currency)
        {
            if (value < 0)
                throw new DomainException("Price value cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainException("Currency cannot be empty.");

           Value = value;
            Currency = currency.ToUpperInvariant();
        }

        public static Price operator +(Price a, Price b)
        {
            if (a.Currency != b.Currency)
                throw new DomainException("Cannot add prices with different currencies.");

            return new Price(a.Value + b.Value, a.Currency);
        }

        public static Price operator -(Price a, Price b)
        {
            if (a.Currency != b.Currency)
                throw new DomainException("Cannot subtract prices with different currencies.");

            return new Price(a.Value - b.Value, a.Currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Currency;
        }

        public override string ToString() => $"{Value} {Currency}";
    }
}

namespace PharmacyApp.Common.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GetEqualityComponents()
                    .Aggregate(17, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
            }
        }
    }
}
    