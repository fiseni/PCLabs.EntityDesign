namespace PCLabs.EntityDesign.Domain.Orders
{
    public class Customer : ValueObject
    {
        private Customer() { }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        public Customer(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
