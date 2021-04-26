using HealthServices.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace HealthServices.Domain
{
    public class PhoneNumber : ValueObject
    {
        private PhoneNumber()
        {
            // required for EF
        }

        public PhoneNumber(string phone)
        {
            if (ValidatePhoneNumberFormat(phone))
            {
                Value = phone;
            }
        }

        public string Value { get; private set; }

        // only validates that the format is correct
        private bool ValidatePhoneNumberFormat(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("PhoneNumber", "PhoneNumber can not be null.");
            }

            if (value.IsValidPhoneNumberFormat())
            {
                return true;
            }

            throw new ArgumentException("The value submitted does not meet the required format.", "PhoneNumber");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
