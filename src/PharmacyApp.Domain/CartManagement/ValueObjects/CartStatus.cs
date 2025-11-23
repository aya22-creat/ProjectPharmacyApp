using PharmacyApp.Common.Common;
using System.Collections.Generic;
using System;

namespace PharmacyApp.Domain.CartManagement.ValueObjects
{
public class CartStatus : ValueObject
{
    public string Value { get; private set; }

    private CartStatus(string value) => Value = value;
    public static CartStatus Active => new CartStatus("Active");
    public static CartStatus CheckedOut => new CartStatus("CheckedOut");
    public static CartStatus Abandoned => new CartStatus("Abandoned");
    public static CartStatus Expired => new CartStatus("Expired");
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(CartStatus status) => status.Value;
}
}
