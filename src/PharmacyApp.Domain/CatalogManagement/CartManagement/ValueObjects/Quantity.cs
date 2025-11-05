using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;


 public class Quantity : ValueObject
    {
        public int Value { get; private set; }

        private Quantity() { } // For EF Core

        public Quantity(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Quantity must be greater than zero", nameof(value));
            
            if (value > 999)
                throw new ArgumentException("Quantity cannot exceed 999", nameof(value));

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public Quantity Increase(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            int newValue = Value + amount;
            
            if (newValue > 999)
                throw new ArgumentException("Quantity cannot exceed 999");

            return new Quantity(newValue);
        }

        public Quantity Decrease(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));

            int newValue = Value - amount;
            
            if (newValue < 1)
                throw new ArgumentException("Quantity cannot be less than 1");

            return new Quantity(newValue);
        }

        public static implicit operator int(Quantity quantity) => quantity.Value;

        public override string ToString() => Value.ToString();
    }