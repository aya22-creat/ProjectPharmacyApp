using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exception;

namespace PharmacyApp.Domain.CatalogManagement.OrderManagement.ValueObjects
{

    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "EGP";

        private Money() { }

        private Money(decimal amount)
        {
            if (amount < 0)
                throw new DomainException("Money amount cannot be negative.");

            Amount = Math.Round(amount, 2);
        }


        public static Money Create(decimal amount)
        {
            return new Money(amount);
        }

        public static Money Zero() => new Money(0);



        public Money Add(Money other)
        {
            EnsureNotNull(other);
            return new Money(Amount + other.Amount);
        }

        public Money Subtract(Money other)
        {
            EnsureNotNull(other);

            if (other.Amount > Amount)
                throw new DomainException("Cannot subtract more than the available amount.");

            return new Money(Amount - other.Amount);
        }

        public Money Multiply(decimal multiplier)
        {
            if (multiplier < 0)
                throw new DomainException("Multiplier cannot be negative.");

            return new Money(Amount * multiplier);
        }

        public Money Multiply(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Quantity cannot be negative.");

            return new Money(Amount * quantity);
        }

        public Money ApplyDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new DomainException("Discount percentage must be between 0 and 100.");

            var discount = Amount * (percentage / 100);
            return new Money(Amount - discount);
        }

        public Money ApplyDiscountAmount(decimal discountAmount)
        {
            if (discountAmount < 0)
                throw new DomainException("Discount amount cannot be negative.");

            if (discountAmount > Amount)
                throw new DomainException("Discount amount cannot exceed the total.");

            return new Money(Amount - discountAmount);
        }

        public Money CalculateTax(decimal taxRate)
        {
            if (taxRate < 0)
                throw new DomainException("Tax rate cannot be negative.");

            var taxAmount = Amount * (taxRate / 100);
            return new Money(Amount + taxAmount);
        }


        public bool IsZero() => Amount == 0;
        public bool IsPositive() => Amount > 0;
        public bool IsNegative() => Amount < 0;

        public bool IsGreaterThan(Money other)
        {
            EnsureNotNull(other);
            return Amount > other.Amount;
        }

        public bool IsLessThan(Money other)
        {
            EnsureNotNull(other);
            return Amount < other.Amount;
        }

        public bool IsEqualTo(Money other)
        {
            EnsureNotNull(other);
            return Amount == other.Amount;
        }


        public static Money operator +(Money left, Money right)
        {
            EnsureNotNull(left);
            EnsureNotNull(right);
            return left.Add(right);
        }

        public static Money operator -(Money left, Money right)
        {
            EnsureNotNull(left);
            EnsureNotNull(right);
            return left.Subtract(right);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            EnsureNotNull(money);
            return money.Multiply(multiplier);
        }

        public static Money operator *(Money money, int quantity)
        {
            EnsureNotNull(money);
            return money.Multiply(quantity);
        }

    

        private static void EnsureNotNull(Money? money)
        {
            if (money == null)
                throw new ArgumentNullException(nameof(money), "Money instance cannot be null.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }



        public override string ToString()
        {
            return $"{Amount:N2} {Currency}";
        }

        public string ToInvoiceFormat()
        {
            return $"{Currency} {Amount:F2}";
        }

      
    }
}
