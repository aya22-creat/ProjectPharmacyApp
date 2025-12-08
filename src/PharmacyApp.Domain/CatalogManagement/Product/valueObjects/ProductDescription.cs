using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;
using System.Linq;
using System.Threading.Tasks;
using PharmacyApp.Common.Common.ValueObjects;



namespace PharmacyApp.Domain.CatalogManagement.Product.ValueObjects;

    public class ProductDescription : ValueObject
    {
        public string Value { get; private set; }

        public ProductDescription(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Product description cannot be empty.", nameof(text));

            if (text.Length > 1000)
                throw new ArgumentException("Product description cannot exceed 1000 characters.", nameof(text));

            Value = text;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }

