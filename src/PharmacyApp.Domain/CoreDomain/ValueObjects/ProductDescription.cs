using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApp.Shared.models;

namespace PharmacyApp.Domain.CoreDomain.ValueObjects
{
    public class ProductDescription : ValueObject
    {
        public string Value { get; private set; }

        public ProductDescription(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw DomainException.ForRuleViolation("ProductDescriptionRequired", "Product description cannot be empty.");

            if (text.Length > 1000)
                throw DomainException.ForRuleViolation("ProductDescriptionTooLong", "Product description cannot exceed 1000 characters.");

            Value = text;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
