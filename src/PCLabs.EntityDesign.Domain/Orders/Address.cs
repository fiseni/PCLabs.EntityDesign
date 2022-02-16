namespace PCLabs.EntityDesign.Domain.Orders
{
    public class Address : ValueObject
    {
        private Address() { }

        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string postalCode, string country)
        {
            if (string.IsNullOrEmpty(street)) throw new ArgumentNullException(nameof(street));
            if (string.IsNullOrEmpty(city)) throw new ArgumentNullException(nameof(city));
            if (string.IsNullOrEmpty(postalCode)) throw new ArgumentNullException(nameof(postalCode));
            if (string.IsNullOrEmpty(country)) throw new ArgumentNullException(nameof(country));

            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return PostalCode;
            yield return Country;
        }
    }
}
