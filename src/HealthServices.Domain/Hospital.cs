using System;

namespace HealthServices.Domain
{
    public class Hospital
    {
        private string _name;

        private Hospital()
        {
            // required for EF
        }

        public Hospital(string name, Address address, PhoneNumber phoneNumber)
        {
            // ensures initial times match
            var currentTime = DateTime.UtcNow;

            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            CreatedTime = currentTime;
            LastModifiedTime = currentTime;
        }

        public Hospital(
            string name,
            string line1,
            string line2,
            string city,
            string state,
            string zipcode, 
            string phoneNumber)
        {
            // ensures initial times match
            var currentTime = DateTime.UtcNow;

            Name = name;
            Address = new Address(line1, line2, city, state, zipcode);
            PhoneNumber = new PhoneNumber(phoneNumber);
            CreatedTime = currentTime;
            LastModifiedTime = currentTime;
        }

        public int Id { get; private set; }
        public string Name 
        { 
            get => _name; 
            private set => _name = !string.IsNullOrWhiteSpace(value)
                ? value.Trim()
                : throw new ArgumentNullException("Name", "Name can not be null.");
        }
        public Address Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime LastModifiedTime { get; private set; }


        public void Update(
            string name,
            string line1,
            string line2,
            string city,
            string state,
            string zipcode,
            string phoneNumber)
        {
            Name = name;
            Address = new Address(line1, line2, city, state, zipcode);
            PhoneNumber = new PhoneNumber(phoneNumber);

            LastModifiedTime = DateTime.UtcNow;
        }
    }
}
