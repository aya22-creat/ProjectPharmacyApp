using PharmacyApp.Common.Common;
using System.Collections.Generic;
using System;


public class CartState : ValueObject
{
    public string Value { get; private set; }

    private CartState(string value) => Value = value;

    public static CartState Active => new CartState("Active");
    public static CartState CheckedOut => new CartState("CheckedOut");
    public static CartState Abandoned => new CartState("Abandoned");
    public static CartState Expired => new CartState("Expired");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(CartState state) => state.Value;
}
