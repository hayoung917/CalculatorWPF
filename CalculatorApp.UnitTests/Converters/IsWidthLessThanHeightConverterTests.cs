using System;
using System.Globalization;
using CalculatorApp.Converters;
using FluentAssertions;
using Xunit;

namespace CalculatorApp.UnitTests.Converters
{
    public class IsWidthLessThanHeightConverterTests
    {
        #region Convert
        [Theory]
        [InlineData(null, null, null, null, false)]
        [InlineData(new object[] { 300, 600 }, null, null, null, true)]
        [InlineData(new object[] { 500, 350 }, null, null, null, false)]
        [InlineData(new object[] { 500.5d, 35.50d }, null, null, null, false)]
        [InlineData(new object[] {"100","200" }, null, null, null, true)]
        [InlineData(new object[] { "100.8", "200.9" }, null, null, null, false)]
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

        [Theory]
        [InlineData(new object[] { }, null, null, null)]
        [InlineData(new object[] { 600 }, null, null, null)]
        [InlineData(new object[] { 600, 900, 1200 }, null, null, null)]
        [InlineData(new object[] { 600, 900, 1200, 1600 }, null, null, null)]

        public void Convert_does_throw_if_values_length_incorrectly(
                object[] values, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            //Arrange
            var sut = new IsWidthLessThanHeightConvert();

            //Act
            var result = new Action(() =>
            {
                sut.Convert(values, targetType, parameter, cultureInfo);
            });

            //Assert
            result.Should().Throw<ArgumentException>();
        }
        #endregion
    }
}
