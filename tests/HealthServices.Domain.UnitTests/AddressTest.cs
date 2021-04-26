using System;
using Xunit;

namespace HealthServices.Domain.UnitTests
{
    public class AddressTest
    {
        [Theory]
        [InlineData("123 Brook St", null, "Norman", "OK", "73072")]
        [InlineData("123 Brook St", "", "Norman", "OK", "85010")]
        [InlineData("123 Brook St", "Bldg 4", "Norman", "OK", "73071-0000")]
        public void New_Valid(
            string line1, 
            string line2,
            string city,
            string state,
            string zipCode)
        {
            var expectedLine2 = string.IsNullOrWhiteSpace(line2) ? null : line2;

            var actual = new Address(line1, line2, city, state, zipCode);

            Assert.Equal(line1, actual.Line1);
            Assert.Equal(expectedLine2, actual.Line2);
            Assert.Equal(city, actual.City);
            Assert.Equal(state, actual.State);
            Assert.Equal(zipCode, actual.ZipCode.Value);
        }

        [Theory]
        [InlineData(null, null, "Norman", "OK", "73072")]
        [InlineData("", "", "Norman", "OK", "85010")]
        [InlineData(" ", "Bldg 4", "Norman", "OK", "73071-0000")]
        public void New_Invalid_Line1(
            string line1,
            string line2,
            string city,
            string state,
            string zipCode)
        {
            var expectedExceptionMessage = "Line1 can not be null. (Parameter 'Line1')";

            var exception = Assert.Throws<ArgumentNullException>(() => new Address(line1, line2, city, state, zipCode));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Theory]
        [InlineData("123 Brook St", null, null, "OK", "73072")]
        [InlineData("123 Brook St", "", "", "OK", "85010")]
        [InlineData("123 Brook St", "Bldg 4", " ", "OK", "73071-0000")]
        public void New_Invalid_City(
            string line1,
            string line2,
            string city,
            string state,
            string zipCode)
        {
            var expectedExceptionMessage = "City can not be null. (Parameter 'City')";

            var exception = Assert.Throws<ArgumentNullException>(() => new Address(line1, line2, city, state, zipCode));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Theory]
        [InlineData("123 Brook St", null, "Norman", null, "73072")]
        [InlineData("123 Brook St", "", "Norman", "", "85010")]
        [InlineData("123 Brook St", "Bldg 4", "Norman", " ", "73071-0000")]
        public void New_Invalid_State(
            string line1,
            string line2,
            string city,
            string state,
            string zipCode)
        {
            var expectedExceptionMessage = "State can not be null. (Parameter 'State')";

            var exception = Assert.Throws<ArgumentNullException>(() => new Address(line1, line2, city, state, zipCode));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Theory]
        [InlineData("123 Brook St", null, "Norman", "OK", null)]
        [InlineData("123 Brook St", "", "Norman", "OK", "")]
        [InlineData("123 Brook St", "Bldg 4", "Norman", "OK", " ")]
        public void New_Invalid_ZipCode(
            string line1,
            string line2,
            string city,
            string state,
            string zipCode)
        {
            var expectedExceptionMessage = "ZipCode can not be null. (Parameter 'ZipCode')";

            var exception = Assert.Throws<ArgumentNullException>(() => new Address(line1, line2, city, state, zipCode));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Fact]
        public void EqualityCheck()
        {
            var first = new Address("123 Brook St", null, "Norman", "OK", "73072");
            var second = new Address("123 Brook St", null, "Norman", "OK", "73072");
            var third = new Address("5555 Main St", null, "Norman", "OK", "73071");

            Assert.Equal(first, second);
            Assert.NotEqual(first, third);
        }
    }
}
