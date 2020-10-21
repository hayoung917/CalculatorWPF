using System;
using CalculatorApp.Models;
using CalculatorApp.ViewModels;
using FluentAssertions;
using Xunit;

namespace CalculatorApp.UnitTests.ViewModels
{
    public class MainViewModelTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct()
        {
            //Arrange

            //Act
            var sut = new Action(() => new MainViewModel());

            //Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region command
        [Theory]
        [InlineData("1", "1")]
        [InlineData("1258", "1258")]
        [InlineData("asdf", " ")]
        [InlineData("0.", "0.")]
        [InlineData("..0", " ")]
        [InlineData(null, " ")]
        [InlineData("..555..", " ")]
        [InlineData("hu8hgygyugt'';;[][]yu", " ")]
        [InlineData("-99", " ")]
        public void Excute_of_InputCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();

            //Act
            sut.InputCommand.Execute(commandParameter);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", " ")]
        [InlineData("1258", " ")]
        [InlineData("asdf", " ")]
        [InlineData("", " ")]
        [InlineData("2.586", " ")]
        [InlineData(null, " ")]
        public void Excute_of_ClearCommand_does_return_correctly(string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();

            //Act
            sut.ClearCommand.Execute(commandParameter);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", " ")]
        [InlineData("1258", "125")]
        [InlineData("12.", "12")]
        [InlineData("asdf", "asd")]
        [InlineData(" ", " ")]
        [InlineData("", " ")]
        [InlineData("hylee", "hyle")]
        [InlineData(null, " ")]
        public void Excute_of_BackspaceCommand_does_return_correctly(
            string commandParameter, string expectedResult)
        {
            //Arrange
            var sut = new MainViewModel();
            sut.NumOutput = commandParameter;
            //Act
            sut.BackspaceCommand.Execute(sut.NumOutput);

            //Assert
            sut.NumOutput.Should().Be(expectedResult);
        }
        #endregion command

        [Fact]
        public void EnumUnits_does_return_EnumUnits()
        {
            //Arrange
            var sut = new MainViewModel();
            var expectedUnit = Enum.GetValues(typeof(TimeUnit));

            //Act
            var result = sut.Units;

            //Assert
            result.Should().BeEquivalentTo(expectedUnit);
        }
    }
}
