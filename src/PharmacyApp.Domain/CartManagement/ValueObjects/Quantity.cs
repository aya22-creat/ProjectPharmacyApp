using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Domain.CartManagement.ValueObjects
{
public class Quantity : ValueObject
{
    public int Value { get; private set; }

    private Quantity() { } // For EF Core

    public Quantity(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(value));

        if (value > 1000)
            throw new ArgumentException("Quantity cannot exceed 1000", nameof(value));

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

        if (newValue > 1000)
            throw new ArgumentException("Quantity cannot exceed 1000");

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
}