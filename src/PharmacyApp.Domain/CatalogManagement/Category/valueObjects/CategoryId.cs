using PharmacyApp.Common.Common;
using PharmacyApp.Common.Common.ValueObjects;

namespace PharmacyApp.Domain.CatalogManagement.Category.ValueObjects;

    public class CategoryId : ValueObject
    {
        public Guid Value { get; private set; }

        private CategoryId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("CategoryId cannot be empty.");

            Value = value;
        }

        public static CategoryId Create(Guid value)
        {
            return new CategoryId(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Guid(CategoryId id) => id.Value;
        public static implicit operator CategoryId(Guid value) => new(value);

    }

