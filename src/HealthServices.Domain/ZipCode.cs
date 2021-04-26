using HealthServices.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace HealthServices.Domain
{
    public class ZipCode : ValueObject
    {
        private ZipCode()
        {
            // required for EF
        }

        public ZipCode(string zipCode)
        {
            if (ValidateZipCodeFormat(zipCode))
            {
                Value = zipCode;
            }
        }

        public string Value { get; private set; }

        // only validates that the format is correct
        private bool ValidateZipCodeFormat(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("ZipCode", "ZipCode can not be null.");
            }

            if (value.IsValidZipCodeFormat())
            {
                return true;
            }

            throw new ArgumentException("The value submitted does not meet the required format.", "ZipCode");
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
