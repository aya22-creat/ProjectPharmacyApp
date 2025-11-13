using System;
using System.Collections.Generic;
using PharmacyApp.Common.Common;

namespace PharmacyApp.Domain.CatalogManagement.CheckoutManagement.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string ZipCode { get; private set; } = null!;

        private Address() { }

        public Address(string street, string city, string country, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street is required.", nameof(street));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.", nameof(country));

            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code is required.", nameof(zipCode));

            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }
    }
}
