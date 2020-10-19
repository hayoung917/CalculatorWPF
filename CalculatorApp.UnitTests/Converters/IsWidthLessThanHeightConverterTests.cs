using System;
using System.Globalization;
using CalculatorApp.Converters;
using FluentAssertions;
using Xunit;

namespace CalculatorApp.UnitTests.Converters
{
    public class IsWidthLessThanHeightConverterTests
    {
        [Theory]
        [InlineData(null, null, null, null, false)]
        [InlineData(new object[] { 300, 600 }, null, null, null, true)]
        [InlineData(new object[] { 500, 350 }, null, null, null, false)]
        public void Convert_does_return_correctly(
            object[] values, Type targetType, object parameter, CultureInfo cultureInfo,
            bool expectedResult)
        {
            //Arrange
            var sut = new IsWidthLessThanHeightConvert();

            //Act
            object result = sut.Convert(values, targetType, parameter, cultureInfo);

            //Assert
            result.Should().Be(expectedResult);
        }
    }
}
