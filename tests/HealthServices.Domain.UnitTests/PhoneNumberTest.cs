using System;
using Xunit;

namespace HealthServices.Domain.UnitTests
{
    public class PhoneNumberTest
    {
        [Theory]
        [InlineData("0000000000")]
        [InlineData("4053649191")]
        public void New_Valid(string expected)
        {
            var actual = new PhoneNumber(expected);

            Assert.Equal(expected, actual.Value);
        }

        [Theory]
        [InlineData("123456789")]
        [InlineData("405-333-4125")]
        [InlineData("A234567899")]
        [InlineData("44455566667")]
        public void New_InvalidFormat(string input)
        {
            var expectedExceptionMessage = "The value submitted does not meet the required format. (Parameter 'PhoneNumber')";

            var exception = Assert.Throws<ArgumentException>(() => new PhoneNumber(input));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void New_InvalidInput(string input)
        {
            var expectedExceptionMessage = "PhoneNumber can not be null. (Parameter 'PhoneNumber')";

            var exception = Assert.Throws<ArgumentNullException>(() => new PhoneNumber(input));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Fact]
        public void EqualityCheck()
        {
            var first = new PhoneNumber("4051234567");
            var second = new PhoneNumber("4051234567");
            var third = new PhoneNumber("9187774444");

            Assert.Equal(first, second);
            Assert.NotEqual(first, third);
        }
    }
}
