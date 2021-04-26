using System;
using Xunit;

namespace HealthServices.Domain.UnitTests
{
    public class ZipCodeTest
    {
        [Theory]
        [InlineData("73072")]
        [InlineData("00001")]
        [InlineData("85010")]
        [InlineData("73071-0000")]
        [InlineData("73071-1004")]
        public void New_Valid(string expected)
        {
            var actual = new ZipCode(expected);

            Assert.Equal(expected, actual.Value);
        }

        [Theory]
        [InlineData("7307")]
        [InlineData("810")]
        [InlineData("0000-1002")]
        [InlineData("730711002")]
        [InlineData("73071-00005")]
        [InlineData("-73071-1004")]
        public void New_InvalidFormat(string input)
        {
            var expectedExceptionMessage = "The value submitted does not meet the required format. (Parameter 'ZipCode')";

            var exception = Assert.Throws<ArgumentException>(() => new ZipCode(input));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void New_InvalidInput(string input)
        {
            var expectedExceptionMessage = "ZipCode can not be null. (Parameter 'ZipCode')";

            var exception = Assert.Throws<ArgumentNullException>(() => new ZipCode(input));

            Assert.Equal(exception.Message, expectedExceptionMessage);
        }

        [Fact]
        public void EqualityCheck()
        {
            var first = new ZipCode("73072");
            var second = new ZipCode("73072");
            var third = new ZipCode("12345");

            Assert.Equal(first, second);
            Assert.NotEqual(first, third);
        }
    }
}
