using System.Text.RegularExpressions;

namespace HealthServices.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidPhoneNumberFormat(this string value)
        {
            string PHONE_NUMBER_REGEX = "^[0-9]{10}$";

            return Regex.IsMatch(value, PHONE_NUMBER_REGEX);
        }

        public static bool IsValidZipCodeFormat(this string value)
        {
            string ZIP_CODE_REGEX = "^[0-9]{5}(?:-[0-9]{4})?$";

            return Regex.IsMatch(value, ZIP_CODE_REGEX);
        }
    }
}
