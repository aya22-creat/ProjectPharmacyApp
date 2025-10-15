using System;
using System.Collections.Generic;
using PharmacyApp.Common.Domain;
using PharmacyApp.Common.Exceptions;

using System.Linq;
using System.Threading.Tasks;


namespace PharmacyApp.Domain.CoreDomain.ValueObjects
{
    public class ProductDescription : ValueObject
    {
        public string value { get; private set; }

        public ProductDescription(string tevxt)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw DomainException.ForRuleViolation("ProductDescriptionRequired", "Product description cannot be empty.");

            if (value.Length > 1000)
                throw DomainException.ForRuleViolation("ProductDescriptionTooLong", "Product description cannot exceed 1000 characters.");

            this.value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return value;
        }

        public override string ToString() => value;
    }
}
    
