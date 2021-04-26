using System;
using System.Collections.Generic;

namespace HealthServices.Domain
{
    public class Address : ValueObject
    {
        private string _line1;
        private string _city;
        private string _state;
        private string _line2;

        private Address()
        {
            // required for EF
        }

        public Address(
            string line1,
            string line2,
            string city,
            string state,
            string zipcode)
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            ZipCode = new ZipCode(zipcode);
        }

        public string Line1
        {
            get => _line1;
            private set => _line1 = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentNullException("Line1", "Line1 can not be null.");
        }

        public string Line2 
        { 
            get => _line2; 
            private set => _line2 = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : null; 
        }

        public string City
        {
            get => _city;
            private set => _city = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentNullException("City", "City can not be null.");
        }

        public string State
        {
            get => _state;
            private set => _state = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentNullException("State", "State can not be null.");
        }

        public ZipCode ZipCode { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Line1;
            yield return Line2;
            yield return City;
            yield return State;
            yield return ZipCode;
        }
    }
}
